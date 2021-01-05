// <copyright file="BinHelper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Binary.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JHackson.Core.Actions;

    /// <summary>
    /// Provides a class to help to work with binary data.
    /// </summary>
    public static class BinHelper
    {
        /// <summary>
        /// Returns the specified 16-bit unsigned integer value as an IEnumerable of bytes.
        /// </summary>
        /// <param name="value">Value whose bytes must be retrieved.</param>
        /// <param name="endianType">Byte order to retrieve.</param>
        /// <returns>An IEnumerable of bytes width length 2.</returns>
        public static IEnumerable<byte> GetBytes(ushort value, EnumEndianType endianType = EnumEndianType.LittleEndian)
        {
            var bytes = BitConverter.GetBytes(value);

            if (endianType == EnumEndianType.BigEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            foreach (byte b in bytes)
            {
                yield return b;
            }
        }

        /// <summary>
        /// Returns the specified 32-bit unsigned integer value as an IEnumerable of bytes.
        /// </summary>
        /// <param name="value">Value whose bytes must be retrieved.</param>
        /// <param name="endianType">Byte order to retrieve.</param>
        /// <returns>An IEnumerable of bytes width length 4.</returns>
        public static IEnumerable<byte> GetBytes(uint value, EnumEndianType endianType = EnumEndianType.LittleEndian)
        {
            var bytes = BitConverter.GetBytes(value);

            if (endianType == EnumEndianType.BigEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            foreach (byte b in bytes)
            {
                yield return b;
            }
        }

        /// <summary>
        /// Returns the specified 64-bit unsigned integer value as an IEnumerable of bytes.
        /// </summary>
        /// <param name="value">Value whose bytes must be retrieved.</param>
        /// <param name="endianType">Byte order to retrieve.</param>
        /// <returns>An IEnumerable of bytes width length 8.</returns>
        public static IEnumerable<byte> GetBytes(ulong value, EnumEndianType endianType = EnumEndianType.LittleEndian)
        {
            var bytes = BitConverter.GetBytes(value);

            if (endianType == EnumEndianType.BigEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            foreach (byte b in bytes)
            {
                yield return b;
            }
        }
    }
}