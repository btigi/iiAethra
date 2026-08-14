using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ii.Aethra
{
    // Title-screen outline
    // Fastgraph packed pixel run (PPR): 4-bit RLE, scanlines bottom-up, 640×350
    public class PillarPpr
    {
        public const int Width = 640;
        public const int Height = 350;

        private static readonly string[] EgaPalette =
        [
            "#000000", "#0000AA", "#00AA00", "#00AAAA",
            "#AA0000", "#AA00AA", "#AA5500", "#AAAAAA",
            "#555555", "#5555FF", "#55FF55", "#55FFFF",
            "#FF5555", "#FF55FF", "#FFFF55", "#FFFFFF"
        ];

        public Image Read(string filename)
        {
            var fileBytes = File.ReadAllBytes(filename);
            return Read(fileBytes);
        }

        public Image Read(byte[] fileBytes)
        {
            var rgbaData = new byte[Height * Width * 4];
            var pixelIndex = 0;
            var totalPixels = Width * Height;

            for (var i = 0; i + 2 < fileBytes.Length && pixelIndex < totalPixels; i += 3)
            {
                var colorA = (fileBytes[i] >> 4) & 0x0F;
                var colorB = fileBytes[i] & 0x0F;
                pixelIndex = EmitRun(rgbaData, pixelIndex, totalPixels, colorA, fileBytes[i + 1]);
                pixelIndex = EmitRun(rgbaData, pixelIndex, totalPixels, colorB, fileBytes[i + 2]);
            }

            return Image.LoadPixelData<Rgba32>(rgbaData, Width, Height);
        }

        public void Write(Image image, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            Write(image, fs);
        }

        public void Write(Image image, Stream stream)
        {
            var rgbaData = new Rgba32[Width * Height];
            image.CloneAs<Rgba32>().CopyPixelDataTo(rgbaData);

            var runs = new List<(byte color, byte count)>();
            byte? current = null;
            var count = 0;

            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < Width; x++)
                {
                    var color = FindClosestPaletteIndex(rgbaData[y * Width + x]);
                    if (current == color && count < 255)
                    {
                        count++;
                        continue;
                    }

                    if (current.HasValue)
                    {
                        runs.Add((current.Value, (byte)count));
                    }

                    current = color;
                    count = 1;
                }
            }

            if (current.HasValue)
            {
                runs.Add((current.Value, (byte)count));
            }

            for (var i = 0; i < runs.Count; i += 2)
            {
                var (colorA, countA) = runs[i];
                byte colorB = 0;
                byte countB = 0;
                if (i + 1 < runs.Count)
                {
                    (colorB, countB) = runs[i + 1];
                }

                stream.WriteByte((byte)((colorA << 4) | colorB));
                stream.WriteByte(countA);
                stream.WriteByte(countB);
            }
        }

        private static int EmitRun(byte[] rgbaData, int pixelIndex, int totalPixels, int paletteIndex, int count)
        {
            var color = EgaPalette[paletteIndex];
            var r = Convert.ToByte(color.Substring(1, 2), 16);
            var g = Convert.ToByte(color.Substring(3, 2), 16);
            var b = Convert.ToByte(color.Substring(5, 2), 16);

            for (var n = 0; n < count && pixelIndex < totalPixels; n++, pixelIndex++)
            {
                var y = Height - 1 - pixelIndex / Width;
                var x = pixelIndex % Width;
                var offset = (y * Width + x) * 4;
                rgbaData[offset] = r;
                rgbaData[offset + 1] = g;
                rgbaData[offset + 2] = b;
                rgbaData[offset + 3] = 255;
            }

            return pixelIndex;
        }

        private static byte FindClosestPaletteIndex(Rgba32 color)
        {
            byte closestIndex = 0;
            var closestDistance = int.MaxValue;

            for (byte i = 0; i < EgaPalette.Length; i++)
            {
                var paletteColor = EgaPalette[i];
                var r = Convert.ToInt32(paletteColor.Substring(1, 2), 16);
                var g = Convert.ToInt32(paletteColor.Substring(3, 2), 16);
                var b = Convert.ToInt32(paletteColor.Substring(5, 2), 16);
                var distance = (color.R - r) * (color.R - r) +
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
