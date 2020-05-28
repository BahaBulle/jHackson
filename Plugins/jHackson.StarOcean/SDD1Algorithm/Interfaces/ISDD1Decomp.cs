// <copyright file="ISDD1Decomp.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System.IO;

    public interface ISDD1Decomp
    {
        MemoryStream Decompress(MemoryStream in_buf, ushort out_len);
    }
}