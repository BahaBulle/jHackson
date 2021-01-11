// <copyright file="DataParameters.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provides parameters to modify binary data in a file.
    /// </summary>
    public class DataParameters
    {
        /// <summary>
        /// Gets or sets the adress of the data.
        /// </summary>
        public long Adress { get; set; }

        /// <summary>
        /// Gets or sets the endian type of the data.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumEndianType Endian { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumDataType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the data.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Check if value is in the correct Type.
        /// </summary>
        /// <returns>True if the value is in the correct Type ; otherwise returns False.</returns>
        public bool Check()
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