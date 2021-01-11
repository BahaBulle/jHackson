// <copyright file="ITilePattern.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image
{
    using System.Collections.Generic;

    public interface ITilePattern
    {
        int Height { get; set; }

        bool Interleave { get; set; }

        List<List<short>> Map { get; }

        EnumTileOrder Order { get; set; }

        int Size { get; set; }

        int Width { get; set; }
    }
}