// <copyright file="Planar2BPPTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Images
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Image;
    using JHackson.Image.ImageFormat;
    using NUnit.Framework;
    using SkiaSharp;

    /// <summary>
    /// Provide a class to test Planar2BPP methods.
    /// </summary>
    public class Planar2BPPTest
    {
        private readonly byte[] hexData = new byte[] { 0x18, 0x10, 0x38, 0x30, 0x38, 0x30, 0x38, 0x30, 0x38, 0x20, 0x30, 0x00, 0x38, 0x30, 0x38, 0x00 };

        private readonly SKColor[] imageData = new SKColor[]
        {
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),    // 11
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x08, 0x52, 0x42, 0xFF),    // 01
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
            new SKColor(0x00, 0x00, 0x00, 0xFF),    // 00
        };

        /// <summary>
        /// Test the conversion of a data into a SKBitmap.
        /// </summary>
        [Test]
        public void ShouldConvertDataToPlanar2BPPImage()
        {
            using (var stream = new MemoryStream(this.hexData))
            {
                var plan = new List<List<short>>()
                {
                    new List<short>()
                    {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        2, 2, 2, 2, 2, 2, 2, 2,
                        4, 4, 4, 4, 4, 4, 4, 4,
                        6, 6, 6, 6, 6, 6, 6, 6,
                        8, 8, 8, 8, 8, 8, 8, 8,
                        10, 10, 10, 10, 10, 10, 10, 10,
                        12, 12, 12, 12, 12, 12, 12, 12,
                        14, 14, 14, 14, 14, 14, 14, 14,
                    },
                    new List<short>()
                    {
                        1, 1, 1, 1, 1, 1, 1, 1,
                        3, 3, 3, 3, 3, 3, 3, 3,
                        5, 5, 5, 5, 5, 5, 5, 5,
                        7, 7, 7, 7, 7, 7, 7, 7,
                        9, 9, 9, 9, 9, 9, 9, 9,
                        11, 11, 11, 11, 11, 11, 11, 11,
                        13, 13, 13, 13, 13, 13, 13, 13,
                        15, 15, 15, 15, 15, 15, 15, 15,
                    },
                };

                var parameters = new ImagePattern()
                {
                    Format = "Planar-2BPP",
                    Height = 8,
                    Width = 8,
                    TilePattern = new TilePattern(plan)
                    {
                        Height = 8,
                        Interleave = true,
                        Order = EnumTileOrder.Planar,
                        Size = 16,
                        Width = 8,
                    },
                };

                var converter = new Planar2BPP();

                var bitmap = converter.Convert(stream, parameters);

                Assert.That(this.imageData, Is.EqualTo(bitmap.Pixels));
            }
        }

        /// <summary>
        /// Test the conversion of a data into a SKBitmap.
        /// </summary>
        [Test]
        public void ShouldConvertPlanar2BPPImageToData()
        {
            var plan = new List<List<short>>()
            {
                new List<short>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0,
                    2, 2, 2, 2, 2, 2, 2, 2,
                    4, 4, 4, 4, 4, 4, 4, 4,
                    6, 6, 6, 6, 6, 6, 6, 6,
                    8, 8, 8, 8, 8, 8, 8, 8,
                    10, 10, 10, 10, 10, 10, 10, 10,
                    12, 12, 12, 12, 12, 12, 12, 12,
                    14, 14, 14, 14, 14, 14, 14, 14,
                },
                new List<short>()
                {
                    1, 1, 1, 1, 1, 1, 1, 1,
                    3, 3, 3, 3, 3, 3, 3, 3,
                    5, 5, 5, 5, 5, 5, 5, 5,
                    7, 7, 7, 7, 7, 7, 7, 7,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    11, 11, 11, 11, 11, 11, 11, 11,
                    13, 13, 13, 13, 13, 13, 13, 13,
                    15, 15, 15, 15, 15, 15, 15, 15,
                },
            };

            var parameters = new ImagePattern()
            {
                Format = "Planar-2BPP",
                Height = 8,
                Width = 8,
                TilePattern = new TilePattern(plan)
                {
                    Height = 8,
                    Interleave = true,
                    Order = EnumTileOrder.Planar,
                    Size = 16,
                    Width = 8,
                },
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

            var converter = new Planar2BPP();

            var dataStream = converter.ConvertBack(bitmap, parameters);

            Assert.That(this.hexData, Is.EqualTo(dataStream.ToArray()));
        }
    }
}