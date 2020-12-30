// <copyright file="StreamExtensions.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Extensions
{
    using System.Collections;
    using System.IO;

    public static class StreamExtensions
    {
        /// <summary>
        /// Read the stream and convert all bytes in an array of bits.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns>Returns an array of bits from the readed stream.</returns>
        public static BitArray GetBitArray(this MemoryStream stream)
        {
            if (stream == null)
            {
                return new BitArray(0);
            }

            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, (int)stream.Length);

            return new BitArray(buffer);
        }
    }
}