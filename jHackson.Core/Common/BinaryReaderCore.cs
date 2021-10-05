// <copyright file="BinaryReaderCore.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Common
{
    using System;
    using System.IO;

    public class BinaryReaderCore : BinaryReader
    {
        public BinaryReaderCore(Stream stream)
            : base(stream)
        {
        }

        public short ReadInt16(EnumEndianType endian)
        {
            if (endian == EnumEndianType.LittleEndian)
            {
                return this.ReadInt16();
            }
            else
            {
                var data = this.ReadBytes(2);
                Array.Reverse(data);

                return BitConverter.ToInt16(data, 0);
            }
        }

        public int ReadInt24(EnumEndianType endian)
        {
            var data = this.ReadBytes(3);

            if (endian == EnumEndianType.BigEndian)
            {
                Array.Reverse(data);
            }

            return BitConverter.ToInt32(data, 0);
        }

        public int ReadInt32(EnumEndianType endian)
        {
            if (endian == EnumEndianType.LittleEndian)
            {
                return this.ReadInt32();
            }
            else
            {
                var data = this.ReadBytes(4);
                Array.Reverse(data);

                return BitConverter.ToInt32(data, 0);
            }
        }

        public long ReadInt64(EnumEndianType endian)
        {
            if (endian == EnumEndianType.LittleEndian)
            {
                return this.ReadInt64();
            }
            else
            {
                var data = this.ReadBytes(8);
                Array.Reverse(data);

                return BitConverter.ToInt64(data, 0);
            }
        }
    }
}