// <copyright file="EvbHeader.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System;
    using JHackson.Core.Exceptions;

    internal class EvbHeader
    {
        private readonly BinaryReader reader;

        internal EvbHeader(BinaryReader binaryReader)
        {
            this.reader = binaryReader;
            this.Id = Array.Empty<byte>();

            this.Parse();
        }

        private void Parse()
        {
            this.Id = this.reader.ReadBytes(4);
            this.Version = this.reader.ReadByte();

            if ((this.Id[0] != 0x1B) || (this.Id[1] != 0x4C) || (this.Id[2] != 0x75) || (this.Id[3] != 0x61) || (this.Version != 0x51))
            {
                throw new JHacksonException("Not a correct EVB file : header");
            }

            this.Format = this.reader.ReadByte();
            this.IsLittleEndian = this.reader.ReadByte() != 0;
            this.SizeOfInt = this.reader.ReadByte();
            this.SizeOfSizeT = this.reader.ReadByte();
            this.SizeOfInstruction = this.reader.ReadByte();
            this.SizeOfLuaNumber = this.reader.ReadByte();
            this.IsIntegral = this.reader.ReadByte() != 0;
        }

        internal byte Format { get; set; }

        internal byte[] Id { get; set; }

        internal bool IsIntegral { get; set; }

        internal bool IsLittleEndian { get; set; }

        internal byte SizeOfInstruction { get; set; }

        internal byte SizeOfInt { get; set; }

        internal byte SizeOfLuaNumber { get; set; }

        internal byte SizeOfSizeT { get; set; }

        internal byte Version { get; set; }
    }
}
