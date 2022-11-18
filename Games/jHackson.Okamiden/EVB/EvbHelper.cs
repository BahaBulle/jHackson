// <copyright file="EvbHelper.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System;

    internal static class EvbHelper
    {
        internal static int ReadInteger(BinaryReader reader, bool isLittleEndian, byte intSize)
        {
            byte[] bytes = reader.ReadBytes(intSize);
            int ret = 0;

            if (isLittleEndian)
            {
                for (int i = 0; i < intSize; ++i)
                {
                    ret += bytes[i] << (i * 8);
                }
            }
            else
            {
                for (int i = 0; i < intSize; ++i)
                {
                    ret += bytes[i] >> (i * 8);
                }
            }

            return ret;
        }

        internal static double ReadNumber(BinaryReader reader, byte numSize)
        {
            byte[] bytes = reader.ReadBytes(numSize);
            double ret;

            if (numSize == 8)
            {
                ret = BitConverter.ToDouble(bytes, 0);
            }
            else if (numSize == 4)
            {
                ret = BitConverter.ToSingle(bytes, 0);
            }
            else
            {
                throw new NotImplementedException("Uhm...");
            }

            return ret;
        }

        internal static string? ReadString(BinaryReader reader, bool isLittleEndian, byte size)
        {
            int stringSize = ReadInteger(reader, isLittleEndian, size);

            if (stringSize == 0) { return null; }

            byte[] bytes = reader.ReadBytes(stringSize);

            char[] chars = new char[bytes.Length];

            for (int i = 0; i < bytes.Length; ++i)
            {
                chars[i] = (char)bytes[i];
            }

            return new string(chars);
        }
    }
}
