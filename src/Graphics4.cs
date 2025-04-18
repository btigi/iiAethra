﻿using SixLabors.ImageSharp;
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
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            var fileBytes = br.ReadBytes((int)fs.Length);
            return Read(fileBytes, imageWidth, imageHeight);
        }

        public List<Image> Read(byte[] fileBytes, int imageWidth, int imageHeight)
        {
            var bytesPerRow = imageWidth / 2; // Each byte encodes two pixels
            var result = new List<Image>();
            int position = 0;

            while (position < fileBytes.Length)
            {
                var rgbaData = DecodeImage(fileBytes, ref position, imageWidth, imageHeight, bytesPerRow);
                var image = Image.LoadPixelData<Rgba32>(rgbaData, imageWidth, imageHeight);
                result.Add(image);
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
    }
}