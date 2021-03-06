﻿// <copyright file="IImageFormat.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image.ImageFormat
{
    using System.IO;
    using SkiaSharp;

    /// <summary>
    /// Interface for image format.
    /// </summary>
    public interface IImageFormat
    {
        /// <summary>
        /// Gets the name of the image format.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Convert data source into the image format.
        /// </summary>
        /// <param name="source">MemoryStream containing data to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns data converted into SKBitmap format.</returns>
        SKBitmap Convert(MemoryStream source, IImagePattern parameters);

        /// <summary>
        /// Convert image source into binary data.
        /// </summary>
        /// <param name="image">Image source to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns image converted in binary data.</returns>
        MemoryStream ConvertBack(SKBitmap image, IImagePattern parameters);
    }
}