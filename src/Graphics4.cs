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

        public List<Image> Read(string filename, int imageWidth, int imageHeight)
        {
            var bytesPerRow = imageWidth / 2; // Each byte encodes two pixels

            var result = new List<Image>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var imagebytes = new int[imageHeight, imageWidth];
                var rgbaData = new byte[imageHeight * imageWidth * 4]; // 4 bytes per pixel (RGBA)

                for (int row = imageHeight - 1; row >= 0; row--) // Bottom to top
                {
                    for (int col = 0; col < bytesPerRow; col++) // Left to right
                    {
                        byte data = br.ReadByte();
                        int leftPixelIndex = (data >> 4) & 0x0F; // High nibble
                        int rightPixelIndex = data & 0x0F;       // Low nibble

                        imagebytes[row, col * 2] = leftPixelIndex;
                        imagebytes[row, col * 2 + 1] = rightPixelIndex;

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

                var image = Image.LoadPixelData<Rgba32>(rgbaData, imageWidth, imageHeight);
                result.Add(image);
            }
            return result;
        }
    }
}