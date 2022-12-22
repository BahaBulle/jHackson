﻿// <copyright file="EvbFile.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using JHackson.Core.Exceptions;

    internal class EvbFile
    {
        private readonly MemoryStream source;

        internal EvbFile(MemoryStream source)
        {
            this.source = source;

            this.Parse();
        }

        internal EvbHeader? Header { get; set; }

        internal EvbFunction? Main { get; set; }

        private void Parse()
        {
            using (var binaryReader = new BinaryReader(this.source))
            {
                if (binaryReader.BaseStream.Length < 12)
                {
                    throw new JHacksonException("Not a correct EVB file : size");
                }

                this.Header = new EvbHeader(binaryReader);
                this.Main = new EvbFunction(binaryReader, this.Header, "Main");
            }
        }
    }
}
