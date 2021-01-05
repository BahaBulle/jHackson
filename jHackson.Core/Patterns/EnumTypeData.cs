// <copyright file="EnumTypeData.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    /// <summary>
    /// Enum to indicate type of data to write.
    /// </summary>
    public enum EnumDataType
    {
        /// <summary>
        /// Indicate to write a byte (8 bits).
        /// </summary>
        U8,

        /// <summary>
        /// Indicate to write unsigned 2 bytes (16 bits).
        /// </summary>
        U16,

        /// <summary>
        /// Indicate to write unsigned 4 bytes (32 bits).
        /// </summary>
        U32,

        /// <summary>
        /// Indicate to write unsigned 8 bytes (64 bits).
        /// </summary>
        U64,

        /// <summary>
        /// Indicate to write a string.
        /// </summary>
        Str,
    }
}