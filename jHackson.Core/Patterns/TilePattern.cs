// <copyright file="TilePattern.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Provides parameters for a tile in a ImagePattern.
    /// </summary>
    public class TilePattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TilePattern"/> class.
        /// </summary>
        public TilePattern()
        {
            this.Map = new List<List<short>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TilePattern"/> class.
        /// </summary>
        /// <param name="map">Map of the tile.</param>
        public TilePattern(List<List<short>> map)
        {
            this.Map = map;
        }

        /// <summary>
        /// Gets or sets the height of the tile (in pixels).
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether bits are interleaved or not.
        /// </summary>
        public bool Interleave { get; set; }

        /// <summary>
        /// Gets the map of the tile.
        /// </summary>
        public List<List<short>> Map { get; private set; }

        /// <summary>
        /// Gets or sets the order of bits in the tile (linear/planar).
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumTileOrder Order { get; set; }

        /// <summary>
        /// Gets or sets the size of the tile (in bytes).
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the width of the tile (in pixels).
        /// </summary>
        public int Width { get; set; }
    }
}