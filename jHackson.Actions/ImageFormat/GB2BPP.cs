﻿// <copyright file="GB2BPP.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.ImageFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using JHackson.Core.Actions;
    using JHackson.Core.ImageFormat;
    using SkiaSharp;

    /// <summary>
    /// Provides a class which convert binary data into image in 2BPP planar (Gameboy).
    /// </summary>
    public class GB2BPP : IImageFormat
    {
        private readonly List<SKColor> defaultPalette = new List<SKColor>()
        {
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x5A, 0x8C, 0xFF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="GB2BPP" /> class.
        /// </summary>
        public GB2BPP()
        {
            this.Name = "2BPP GB";
        }

        /// <summary>
        /// Gets the name of this format.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Convert binary data into an image in 2BPP planar (Gameboy).
        /// </summary>
        /// <param name="ms">MemoryStream which contains data.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Return the data converted into a SKBitmap.</returns>
        public SKBitmap Convert(MemoryStream ms, ImageParameters parameters)
        {
            SKBitmap bitmap = null;

            if (parameters == null)
            {
                return null;
            }

            using (var binaryReader = new BinaryReader(ms, Encoding.UTF8, true))
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
                            var bytes = binaryReader
                                .ReadBytes(2)
                                .Reverse()
                                .ToArray();

                            for (var width = 0; width < tileWidth; width++)
                            {
                                var bit1 = (bytes[0] >> (tileWidth - 1 - width)) & 0x01;
                                var bit2 = (bytes[1] >> (tileWidth - 1 - width)) & 0x01;

                                int bitColor = (bit1 << 1) | bit2;

                                SKColor color;
                                if (parameters.Palette.Count > 0)
                                {
                                    color = parameters.Palette[bitColor];
                                }
                                else
                                {
                                    color = this.defaultPalette[bitColor];
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
    }
}