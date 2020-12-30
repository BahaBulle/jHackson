// <copyright file="Linear1BPP.cs" company="BahaBulle">
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
    /// Provides a class which convert binary data into image in 1BPP
    /// </summary>
    public class Linear1BPP : IImageFormat
    {
        private readonly List<SKColor> defaultPalette = new List<SKColor>()
        {
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Linear1BPP"/> class.
        /// </summary>
        public Linear1BPP()
        {
            this.Name = "Linear-1BPP";
        }

        /// <summary>
        /// Gets the name of this format.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Convert binary data into an image in 1BPP linear.
        /// </summary>
        /// <param name="stream">MemoryStream which contains data.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Return the data converted into a SKBitmap.</returns>
        public SKBitmap Convert(MemoryStream stream, ImagePattern parameters)
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

                int tileHeight = parameters.TilePattern.Height;
                int tileWidth = parameters.TilePattern.Width;
                int numberOfRowsOfTiles = parameters.Height / tileHeight;
                int numberOfColsOfTiles = parameters.Width / tileWidth;

                var pixels = bitmap.Pixels;

                for (var row = 0; row < numberOfRowsOfTiles; row++)
                {
                    for (var column = 0; column < numberOfColsOfTiles; column++)
                    {
                        for (var height = 0; height < tileHeight; height++)
                        {
                            var byteRead = binaryReader.ReadByte();

                            for (var width = 0; width < tileWidth; width++)
                            {
                                var indexColor = (byteRead >> (tileWidth - width - 1)) & 0x01;

                                SKColor color;
                                if (parameters.Palette.Count > 0)
                                {
                                    color = parameters.Palette[indexColor];
                                }
                                else
                                {
                                    color = this.defaultPalette[indexColor];
                                }

                                pixels[(height * parameters.Width) + width + (column * tileWidth) + (row * tileHeight * parameters.Width)] = color;
                            }
                        }
                    }
                }

                bitmap.Pixels = pixels;
            }

            return bitmap;
        }

        /// <summary>
        /// Convert an image in 1BPP linear into binary data.
        /// </summary>
        /// <param name="image">Image source to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns image converted in binary data.</returns>
        public MemoryStream ConvertBack(SKBitmap image, ImagePattern parameters)
        {
            if (image == null || parameters == null)
            {
                return null;
            }

            var stream = new MemoryStream();

            int tileHeight = parameters.TilePattern.Height;
            int tileWidth = parameters.TilePattern.Width;
            int numberOfRowsOfTiles = parameters.Height / tileHeight;
            int numberOfColsOfTiles = parameters.Width / tileWidth;

            var pixels = image.Pixels;

            for (var row = 0; row < numberOfRowsOfTiles; row++)
            {
                for (var column = 0; column < numberOfColsOfTiles; column++)
                {
                    for (var height = 0; height < tileHeight; height++)
                    {
                        byte byteToWrite = 0;

                        for (var width = 0; width < tileWidth; width++)
                        {
                            var color = pixels[(height * parameters.Width) + width + (column * tileWidth) + (row * tileHeight * parameters.Width)];

                            int bit;
                            if (parameters.Palette.Count > 0)
                            {
                                bit = parameters.Palette.IndexOf(color);
                            }
                            else
                            {
                                bit = this.defaultPalette.IndexOf(color);
                            }

                            byteToWrite |= (byte)(bit << (tileWidth - width - 1));
                        }

                        stream.WriteByte(byteToWrite);
                    }
                }
            }

            return stream;
        }
    }
}