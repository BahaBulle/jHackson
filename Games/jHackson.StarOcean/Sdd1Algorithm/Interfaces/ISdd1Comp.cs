// <copyright file="ISdd1Comp.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System.IO;

    public interface ISdd1Comp
    {
        MemoryStream Compress(MemoryStream bufferIn);
    }
}