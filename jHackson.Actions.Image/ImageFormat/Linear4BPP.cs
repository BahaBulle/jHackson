// <copyright file="Linear4BPP.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Image.ImageFormat
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using JHackson.Core.Actions;
    using JHackson.Core.ImageFormat;
    using SkiaSharp;

    /// <summary>
    /// Provides a class which convert binary data into image in 4BPP linear.
    /// </summary>
    public class Linear4BPP : IImageFormat
    {
        private readonly List<SKColor> defaultPalette = new List<SKColor>()
        {
            new SKColor(0x6B, 0x6B, 0x6B, 0xFF),
            new SKColor(0x00, 0x10, 0x84, 0xFF),
            new SKColor(0x08, 0x00, 0x8C, 0xFF),
            new SKColor(0x42, 0x00, 0x7B, 0xFF),
            new SKColor(0x63, 0x00, 0x5A, 0xFF),
            new SKColor(0x6B, 0x00, 0x10, 0xFF),
            new SKColor(0x63, 0x00, 0x00, 0xFF),
            new SKColor(0x4A, 0x31, 0x00, 0xFF),
            new SKColor(0x31, 0x4A, 0x18, 0xFF),
            new SKColor(0x00, 0x5A, 0x21, 0xFF),
            new SKColor(0x21, 0x5A, 0x10, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x00, 0x39, 0x73, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x00, 0x00, 0x00, 0xFF),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Linear4BPP"/> class.
        /// </summary>
        public Linear4BPP()
        {
            this.Name = "Linear-4BPP";
        }

        /// <summary>
        /// Gets the name of this format.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Convert binary data into an image in 4BPP (Genesis).
        /// </summary>
        /// <param name="stream">MemoryStream which contains data.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Return the data converted into a SKBitmap.</returns>
        public SKBitmap Convert(MemoryStream stream, ImageParameters parameters)
        {
            if (stream == null || parameters == null)
            {
                return null;
            }

            SKBitmap bitmap = null;

            using (var binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                var imageInfo = new SKImageInfo(parameters.Width, parameters.Height, SKColorType.Rgba8888);

                bitmap = new SKBitmap(imageInfo);

                int tileHeight = parameters.TileHeight;
                int tileWidth = parameters.TileWidth;
                int numberOfRowsOfTiles = parameters.Height / tileHeight;
                int numberOfColsOfTiles = parameters.Width / tileWidth;

                var pixels = bitmap.Pixels;

                for (var row = 0; row < numberOfRowsOfTiles; row++)
                {
                    for (var column = 0; column < numberOfColsOfTiles; column++)
                    {
                        for (var height = 0; height < tileHeight; height++)
                        {
                            for (var width = 0; width < tileWidth; width += 2)
                            {
                                var byteRead = binaryReader.ReadByte();

                                var indexColor1 = (byteRead >> 4) & 0x0F;
                                var indexColor2 = byteRead & 0x0F;

                                SKColor color1;
                                SKColor color2;
                                if (parameters.Palette.Count > 0)
                                {
                                    color1 = parameters.Palette[indexColor1];
                                    color2 = parameters.Palette[indexColor2];
                                }
                                else
                                {
                                    color1 = this.defaultPalette[indexColor1];
                                    color2 = this.defaultPalette[indexColor2];
                                }

                                pixels[(height * parameters.Width) + width + (column * tileWidth) + (row * tileHeight * parameters.Width)] = color1;
                                pixels[(height * parameters.Width) + width + 1 + (column * tileWidth) + (row * tileHeight * parameters.Width)] = color2;
                            }
                        }
                    }
                }

                bitmap.Pixels = pixels;
            }

            return bitmap;
        }

        /// <summary>
        /// Convert an image in 4BPP (Genesis) into binary data.
        /// </summary>
        /// <param name="image">Image source to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns image converted in binary data.</returns>
        public MemoryStream ConvertBack(SKBitmap image, ImageParameters parameters)
        {
            if (image == null || parameters == null)
            {
                return null;
            }

            var stream = new MemoryStream();

            int tileHeight = parameters.TileHeight;
            int tileWidth = parameters.TileWidth;
            int numberOfRowsOfTiles = parameters.Height / tileHeight;
            int numberOfColsOfTiles = parameters.Width / tileWidth;

            var pixels = image.Pixels;

            for (var row = 0; row < numberOfRowsOfTiles; row++)
            {
                for (var column = 0; column < numberOfColsOfTiles; column++)
                {
                    for (var height = 0; height < tileHeight; height++)
                    {
                        for (var width = 0; width < tileWidth; width += 2)
                        {
                            var color1 = pixels[(height * parameters.Width) + width + (column * tileWidth) + (row * tileHeight * parameters.Width)];
                            var color2 = pixels[(height * parameters.Width) + width + 1 + (column * tileWidth) + (row * tileHeight * parameters.Width)];

                            int bit1;
                            int bit2;
                            if (parameters.Palette.Count > 0)
                            {
                                bit1 = parameters.Palette.IndexOf(color1);
                                bit2 = parameters.Palette.IndexOf(color2);
                            }
                            else
                            {
                                bit1 = this.defaultPalette.IndexOf(color1);
                                bit2 = this.defaultPalette.IndexOf(color2);
                            }

                            var byteToWrite = (bit1 << 4) | bit2;

                            stream.WriteByte((byte)byteToWrite);
                        }
                    }
                }
            }

            return stream;
        }
    }
}