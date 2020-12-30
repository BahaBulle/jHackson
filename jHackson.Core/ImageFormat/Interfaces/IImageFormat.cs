// <copyright file="IImageFormat.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.ImageFormat
{
    using System.IO;
    using JHackson.Core.Actions;
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
        SKBitmap Convert(MemoryStream source, ImagePattern parameters);

        /// <summary>
        /// Convert image source into binary data.
        /// </summary>
        /// <param name="image">Image source to convert.</param>
        /// <param name="parameters">Parameters for the image.</param>
        /// <returns>Returns image converted in binary data.</returns>
        MemoryStream ConvertBack(SKBitmap image, ImagePattern parameters);
    }
}