// <copyright file="BinaryReaderExtension.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.Extensions
{
    using System.IO;

    public static class BinaryReaderExtension
    {
        public static byte PeekByte(this BinaryReader reader)
        {
            var pos = reader.BaseStream.Position;

            var value = reader.ReadByte();

            reader.BaseStream.Position = pos;

            return value;
        }

        public static byte PeekByte(this BinaryReader reader, int posToAdd)
        {
            var pos = reader.BaseStream.Position;

            reader.BaseStream.Position += posToAdd;

            var value = reader.ReadByte();

            reader.BaseStream.Position = pos;

            return value;
        }
    }
}