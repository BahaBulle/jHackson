// <copyright file="ImageParameters.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    using System.Collections.Generic;
    using JHackson.Core.Json.JsonConverters;
    using Newtonsoft.Json;
    using SkiaSharp;

    /// <summary>
    /// Provides parameters for an image in ordre to load/save it in a file.
    /// </summary>
    public class ImagePattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePattern" /> class.
        /// </summary>
        public ImagePattern()
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
        [JsonConverter(typeof(SKColorJsonConverter))]
        [JsonProperty]
        public List<SKColor> Palette { get; private set; }

        /// <summary>
        /// Gets or sets the height of a tile (in pixels).
        /// </summary>
        //public int TileHeight { get; set; }

        /// <summary>
        /// Gets or sets the pattern of a tile in the image.
        /// </summary>
        public TilePattern TilePattern { get; set; }

        /// <summary>
        /// Gets or sets the width of a tile (in pixels).
        /// </summary>
        //public int TileWidth { get; set; }

        /// <summary>
        /// Gets or sets the width of the image (in pixels).
        /// </summary>
        public int Width { get; set; }
    }
}