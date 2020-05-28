// <copyright file="ISDD1Comp.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System.IO;

    public interface ISDD1Comp
    {
        MemoryStream Compress(MemoryStream in_buf);
    }
}