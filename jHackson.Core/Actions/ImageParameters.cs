// <copyright file="ImageParameters.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    using System.Collections.Generic;
    using SkiaSharp;

    /// <summary>
    /// Provides parameters for an image in ordre to load/save it in a file.
    /// </summary>
    public class ImageParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageParameters" /> class.
        /// </summary>
        public ImageParameters()
        {
            this.Palette = new List<SKColor>();
        }

        /// <summary>
        /// Gets or sets the format of the data.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the height of the image (in pixels).
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets the palette to use.
        /// </summary>
        public List<SKColor> Palette { get; }

        /// <summary>
        /// Gets or sets the height of a tile (in pixels).
        /// </summary>
        public int TileHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of a tile (in pixels).
        /// </summary>
        public int TileWidth { get; set; }

        /// <summary>
        /// Gets or sets the width of the image (in pixels).
        /// </summary>
        public int Width { get; set; }
    }
}