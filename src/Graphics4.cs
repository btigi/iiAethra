using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace iiAethra
{
    public class Graphics4
    {
        private readonly string[] palette =
        [
            "#FF00FF", "#0000A8", "#00A800", "#00A8FC",
            "#A85454", "#FCA800", "#A85400", "#A8A8A8",
            "#545454", "#5454FC", "#54FC54", "#000000",
            "#FC5454", "#FCA8A8", "#FCFC00", "#FCFCFC"
        ];

        public List<Image> Read(string filename, List<(int width, int height, int count)> imageSizes)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            var fileBytes = br.ReadBytes((int)fs.Length);
            return Read(fileBytes, imageSizes);
        }

        public List<Image> Read(byte[] fileBytes, List<(int width, int height, int count)> imageSizes)
        {
            var result = new List<Image>();
            int position = 0;

            foreach (var (width, height, count) in imageSizes)
            {
                var bytesPerRow = width / 2; // Each byte encodes two pixels  
                int totalImages = count == -1 ? int.MaxValue : count;

                for (int i = 0; i < totalImages; i++)
                {
                    if (position >= fileBytes.Length)
                        break;

                    var rgbaData = DecodeImage(fileBytes, ref position, width, height, bytesPerRow);
                    var image = Image.LoadPixelData<Rgba32>(rgbaData, width, height);
                    result.Add(image);
                }
            }

            return result;
        }

        private byte[] DecodeImage(byte[] fileBytes, ref int position, int imageWidth, int imageHeight, int bytesPerRow)
        {
            var rgbaData = new byte[imageHeight * imageWidth * 4]; // 4 bytes per pixel (RGBA)  

            for (int row = imageHeight - 1; row >= 0; row--) // Bottom to top  
            {
                for (int col = 0; col < bytesPerRow; col++) // Left to right  
                {
                    if (position >= fileBytes.Length)
                        break;

                    byte data = fileBytes[position++];
                    int leftPixelIndex = (data >> 4) & 0x0F; // High nibble  
                    int rightPixelIndex = data & 0x0F;       // Low nibble  

                    // Convert palette index to RGBA for left pixel  
                    var leftColor = palette[leftPixelIndex];
                    rgbaData[(row * imageWidth + col * 2) * 4 + 0] = Convert.ToByte(leftColor.Substring(1, 2), 16); // R  
                    rgbaData[(row * imageWidth + col * 2) * 4 + 1] = Convert.ToByte(leftColor.Substring(3, 2), 16); // G  
                    rgbaData[(row * imageWidth + col * 2) * 4 + 2] = Convert.ToByte(leftColor.Substring(5, 2), 16); // B  
                    rgbaData[(row * imageWidth + col * 2) * 4 + 3] = 255; // A (fully opaque)  

                    // Convert palette index to RGBA for right pixel  
                    var rightColor = palette[rightPixelIndex];
                    rgbaData[(row * imageWidth + col * 2 + 1) * 4 + 0] = Convert.ToByte(rightColor.Substring(1, 2), 16); // R  
                    rgbaData[(row * imageWidth + col * 2 + 1) * 4 + 1] = Convert.ToByte(rightColor.Substring(3, 2), 16); // G  
                    rgbaData[(row * imageWidth + col * 2 + 1) * 4 + 2] = Convert.ToByte(rightColor.Substring(5, 2), 16); // B  
                    rgbaData[(row * imageWidth + col * 2 + 1) * 4 + 3] = 255; // A (fully opaque)  
                }
            }

            return rgbaData;
        }

        public void Write(List<Image> images, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);

            foreach (var image in images)
            {
                var pixelData = new byte[image.Width * image.Height / 2]; // Each byte encodes two pixels
                var rgbaData = new Rgba32[image.Width * image.Height];

                var pixelIndex = 0;
                image.CloneAs<Rgba32>().CopyPixelDataTo(rgbaData); // Copy pixel data to rgbaData
                for (int row = image.Height - 1; row >= 0; row--) // Top to bottom
                {
                    for (int col = 0; col < image.Width; col += 2) // Two pixels per byte
                    {
                        int leftIndex = row * image.Width + col;
                        int rightIndex = leftIndex + 1;

                        byte leftPixel = FindClosestPaletteIndex(rgbaData[leftIndex]);
                        byte rightPixel = col + 1 < image.Width ? FindClosestPaletteIndex(rgbaData[rightIndex]) : (byte)0;

                        pixelData[pixelIndex++] = (byte)((leftPixel << 4) | rightPixel);
                    }
                }

                bw.Write(pixelData);
            }
        }

        private byte FindClosestPaletteIndex(Rgba32 color)
        {
            byte closestIndex = 0;
            int closestDistance = int.MaxValue;

            for (byte i = 0; i < palette.Length; i++)
            {
                var paletteColor = palette[i];
                int r = Convert.ToInt32(paletteColor.Substring(1, 2), 16);
                int g = Convert.ToInt32(paletteColor.Substring(3, 2), 16);
                int b = Convert.ToInt32(paletteColor.Substring(5, 2), 16);

                int distance = (color.R - r) * (color.R - r) +
                               (color.G - g) * (color.G - g) +
                               (color.B - b) * (color.B - b);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
    }
}