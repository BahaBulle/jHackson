// <copyright file="Adress.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Common
{
    using System.IO;
    using JHackson.Core.Json.JsonConverters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Provides a class that allows to set an adress and his origin.
    /// </summary>
    public class Adress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adress" /> class.
        /// </summary>
        public Adress()
        {
            this.Origin = SeekOrigin.Begin;
        }

        /// <summary>
        /// Gets or sets the origin of the adress.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SeekOrigin Origin { get; set; }

        /// <summary>
        /// Gets or sets the adress of the data.
        /// </summary>
        [JsonConverter(typeof(AdressJsonConverter))]
        public long Value { get; set; }
    }
}