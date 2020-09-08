﻿namespace jHackson.Tests.Images
{
    using System.IO;
    using JHackson.Actions.Image.ImageFormat;
    using JHackson.Core.Actions;
    using NUnit.Framework;
    using SkiaSharp;

    /// <summary>
    /// Class to test GB2BPP methods
    /// </summary>
    public class TestG2BPP
    {
        private readonly byte[] hexData = new byte[] { 0x18, 0x10, 0x38, 0x30, 0x38, 0x30, 0x38, 0x30, 0x38, 0x20, 0x30, 0x00, 0x38, 0x30, 0x38, 0x00 };

        private readonly SKColor[] imageData = new SKColor[]
        {
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
        };

        /// <summary>
        /// Test the conversion of a data into a SKBitmap
        /// </summary>
        [Test]
        public void ShouldConvertDataToGB2BPPImage()
        {
            byte[] data = new byte[] { 0x18, 0x10, 0x38, 0x30, 0x38, 0x30, 0x38, 0x30, 0x38, 0x20, 0x30, 0x00, 0x38, 0x30, 0x38, 0x00 };

            using (var stream = new MemoryStream(data))
            {
                var parameters = new ImageParameters()
                {
                    Format = "2BPP GB",
                    Height = 8,
                    TileHeight = 8,
                    TileWidth = 8,
                    Width = 8,
                };

                var converter = new GB2BPP();

                var bitmap = converter.Convert(stream, parameters);

                Assert.That(this.imageData, Is.EqualTo(bitmap.Pixels));
            }
        }

        /// <summary>
        /// Test the conversion of a data into a SKBitmap
        /// </summary>
        [Test]
        public void ShouldConvertGB2BPPImageToData()
        {
            var parameters = new ImageParameters()
            {
                Format = "2BPP GB",
                Height = 8,
                TileHeight = 8,
                TileWidth = 8,
                Width = 8,
            };

            var imageInfo = new SKImageInfo(parameters.Width, parameters.Height, SKColorType.Rgba8888);

            var bitmap = new SKBitmap(imageInfo);

            var pixels = bitmap.Pixels;
            int i = 0;

            foreach (var color in this.imageData)
            {
                pixels[i++] = color;
            }

            bitmap.Pixels = pixels;

            var converter = new GB2BPP();

            var dataStream = converter.ConvertBack(bitmap, parameters);

            Assert.That(this.hexData, Is.EqualTo(dataStream.ToArray()));
        }
    }
}