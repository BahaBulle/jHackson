// <copyright file="DataParameters.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provides parameters to modify binary data in a file.
    /// </summary>
    public class DataParameters
    {
        public const string SEEK_END = "SEEK_END";

        private readonly Regex numberRegex = new Regex(@"^\d+$");

        public DataParameters()
        {
        }

        public DataParameters(string adress)
        {
            this.AdressValue = adress;
        }

        /// <summary>
        /// Gets the adress of the data.
        /// </summary>
        [JsonIgnore]
        public long Adress { get; private set; }

        /// <summary>
        /// Gets or sets the endian type of the data.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumEndianType Endian { get; set; }

        /// <summary>
        /// Gets the origin of the seek.
        /// </summary>
        [JsonIgnore]
        public SeekOrigin Origin { get; private set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumDataType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the data.
        /// </summary>
        public object Value { get; set; }

        [JsonProperty("Adress")]
        private string AdressValue { get; set; }

        /// <summary>
        /// Check if the value is in a correct format.
        /// </summary>
        /// <returns>True if the format is correct; False otherwise.</returns>
        public bool CheckAdress()
        {
            if (this.AdressValue == SEEK_END)
            {
                this.Adress = 0;
                this.Origin = SeekOrigin.End;
                return true;
            }
            else if (this.numberRegex.IsMatch(this.AdressValue))
            {
                bool result = long.TryParse(this.AdressValue, out long value);

                if (result)
                {
                    this.Adress = value;
                    this.Origin = SeekOrigin.Begin;
                }

                return true;
            }
            else if (this.AdressValue.StartsWith("0x", true, CultureInfo.InvariantCulture))
            {
                bool result = long.TryParse(this.AdressValue.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long value);

                if (result)
                {
                    this.Origin = SeekOrigin.Begin;
                    this.Adress = value;
                }

                return result;
            }

            return false;
        }

        /// <summary>
        /// Check if value is in the correct Type.
        /// </summary>
        /// <returns>True if the value is in the correct Type ; otherwise returns False.</returns>
        public bool CheckType()
        {
            try
            {
                switch (this.Type)
                {
                    case EnumDataType.Array8:
                    case EnumDataType.Array16:
                    case EnumDataType.Array32:
                    case EnumDataType.Array64:
                        if (!(this.Value is JArray))
                        {
                            return false;
                        }

                        foreach (var value in (JArray)this.Value)
                        {
                            if (value.Type != JTokenType.Integer)
                            {
                                return false;
                            }
                        }

                        break;

                    case EnumDataType.U8:
                        _ = Convert.ToByte(this.Value, CultureInfo.CurrentCulture);
                        break;

                    case EnumDataType.U16:
                        _ = Convert.ToUInt16(this.Value, CultureInfo.CurrentCulture);
                        break;

                    case EnumDataType.U32:
                        _ = Convert.ToUInt32(this.Value, CultureInfo.CurrentCulture);
                        break;

                    case EnumDataType.U64:
                        _ = Convert.ToUInt64(this.Value, CultureInfo.CurrentCulture);
                        break;

                    case EnumDataType.Str:
                        if (!(this.Value is string))
                        {
                            return false;
                        }

                        break;
                }

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}