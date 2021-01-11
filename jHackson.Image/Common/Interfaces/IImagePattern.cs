// <copyright file="IImagePattern.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image
{
    using System.Collections.Generic;
    using SkiaSharp;

    public interface IImagePattern
    {
        string Format { get; set; }

        int Height { get; set; }

        List<SKColor> Palette { get; }

        TilePattern TilePattern { get; set; }

        int Width { get; set; }
    }
}