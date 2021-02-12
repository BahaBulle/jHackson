// <copyright file="AdressJsonConverter.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.JsonConverters
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using JHackson.Core.Common;
    using Newtonsoft.Json;

    /// <summary>
    /// Converts an string or long value to and from an <see cref="Adress"/>.
    /// </summary>
    public class AdressJsonConverter : JsonConverter
    {
        /// <summary>
        /// Constante to indicate the origin of the seeking adress.
        /// </summary>
        public const string SEEK_END = "SEEK_END";

        private readonly Regex numberRegex = new Regex(@"^\d+$");

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can read Json.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can write Json.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Adress);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            try
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                if (reader.TokenType == JsonToken.String)
                {
                    string text = reader.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return null;
                    }

                    if (this.numberRegex.IsMatch(text))
                    {
                        bool result = long.TryParse(text, out long number);

                        if (result)
                        {
                            return number;
                        }
                    }
                    else if (text.StartsWith("0x", true, CultureInfo.InvariantCulture))
                    {
                        bool result = long.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long number);

                        if (result)
                        {
                            return number;
                        }
                    }

                    return null;
                }

                if (reader.TokenType == JsonToken.Integer)
                {
                    bool result = long.TryParse(reader.Value?.ToString(), out long number);

                    if (result)
                    {
                        return number;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue($"0x{value:X}");
        }
    }
}