// <copyright file="Planar2BPP.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image.ImageFormat
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.TableElements.Extensions;
    using SkiaSharp;

    /// <summary>
    /// Provides a class which convert binary data into image in 2BPP planar (Gameboy).
    /// </summary>
    public class Planar2BPP : IImageFormat
    {
        private readonly List<SKColor> defaultPalette = new List<SKColor>()
        {
            new SKColor(0x00, 0x00, 0x00, 0xFF),
            new SKColor(0x08, 0x52, 0x42, 0xFF),
            new SKColor(0x5A, 0x8C, 0xFF, 0xFF),
            new SKColor(0xEF, 0xEF, 0xEF, 0xFF),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Planar2BPP" /> class.
        /// </summary>
        public Planar2BPP()
        {
            this.Name = "Planar-2BPP";
        }

        /// <summary>
        /// Gets the name of this format.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Convert binary data into an image in 2BPP planar (Gameboy).
        /// </summary>
        /// <param name="stream">MemoryStream which contains data.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Return the data converted into a SKBitmap.</returns>
        public SKBitmap Convert(MemoryStream stream, IImagePattern parameters)
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

                int numberOfTiles = numberOfRowsOfTiles * numberOfColsOfTiles;
                int numberTotalOfBit = numberOfTiles * parameters.TilePattern.Size * 8;

                var imageBits = new BitList(numberTotalOfBit, false);

                var imageTiles = new BitList();
                for (int i = 0; i < numberOfTiles; i++)
                {
                    var tileBytes = binaryReader.ReadBytes(parameters.TilePattern.Size);
                    var tileBits = new BitList(tileBytes);

                    var tile = tileBits.RearrangeBitsWith2Planes(parameters.TilePattern);

                    imageTiles.AddBitList(tile);
                }

                int index = 0;
                for (var row = 0; row < numberOfRowsOfTiles; row++)
                {
                    for (var column = 0; column < numberOfColsOfTiles; column++)
                    {
                        for (var height = 0; height < tileHeight; height++)
                        {
                            for (var width = 0; width < tileWidth; width++)
                            {
                                var bitColor = System.Convert.ToInt32(imageTiles.GetBinaryString(index++) + imageTiles.GetBinaryString(index++), 2);

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

        /// <summary>
        /// Convert an image in 2BPP planar (Gameboy) into binary data.
        /// </summary>
        /// <param name="image">Image source to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns image converted in binary data.</returns>
        public MemoryStream ConvertBack(SKBitmap image, IImagePattern parameters)
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

            int numberOfTiles = numberOfRowsOfTiles * numberOfColsOfTiles;
            int numberTotalOfBit = numberOfTiles * parameters.TilePattern.Size * 8;

            var imageBits = new BitList();

            for (var row = 0; row < numberOfRowsOfTiles; row++)
            {
                for (var column = 0; column < numberOfColsOfTiles; column++)
                {
                    var tile = new BitList();
                    for (var height = 0; height < tileHeight; height++)
                    {
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

                            var bitString = System.Convert.ToString(bit, 2).PadLeft(2, '0');
                            var bits = bitString.SplitByLength(1);

                            foreach (var b in bits)
                            {
                                tile.Add(b == "1" ? true : false);
                            }
                        }
                    }

                    if (tile.Count != parameters.TilePattern.Size * 8)
                    {
                        throw new JHacksonException(LocalizationManager.GetMessage("image.tile.incorrectNumberOfBits", row, column, imageBits.Count, numberTotalOfBit));
                    }

                    imageBits.AddBitList(tile.RearrangeBitsWith2PlanesBack(parameters.TilePattern));
                }
            }

            if (imageBits.Count != numberTotalOfBit)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectBitsImageSize", imageBits.Count, numberTotalOfBit));
            }

            var bytes = imageBits.ToBytes();

            stream.Write(bytes, 0, bytes.Length);

            return stream;
        }
    }
}