// <copyright file="ISdd1Decomp.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System.IO;

    public interface ISdd1Decomp
    {
        MemoryStream Decompress(MemoryStream bufferIn, ushort outLen);
    }
}