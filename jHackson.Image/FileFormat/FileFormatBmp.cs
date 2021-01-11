// <copyright file="FileFormatBmp.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image.FileFormat
{
    using System.IO;
    using BmpSharp;
    using JHackson.Core.Exceptions;
    using JHackson.Core.FileFormat;
    using JHackson.Core.Localization;
    using SkiaSharp;

    /// <summary>
    /// Provides a format which allows to save a MemoryStream in a BMP file.
    /// </summary>
    public class FileFormatBmp : IFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFormatBmp"/> class.
        /// </summary>
        public FileFormatBmp()
        {
            this.Name = "Bmp";
        }

        /// <summary>
        /// Gets or sets the name of the format.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Save the MemoryStream into a BMP file.
        /// </summary>
        /// <param name="filename">Filename of the BMP file created.</param>
        /// <param name="buffer">MemoryStream to convert into BMP file.</param>
        public void Save(string filename, object buffer)
        {
            if (buffer is SKBitmap bitmap)
            {
                var directory = Path.GetDirectoryName(filename);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (!(bitmap.BytesPerPixel == 3 || bitmap.BytesPerPixel == 4))
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectBitsPerImage", bitmap.BytesPerPixel));
                }

                var bitsPerPixel = bitmap.BytesPerPixel == 4 ? BitsPerPixelEnum.RGBA32 : BitsPerPixelEnum.RGB24;

                var bmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.Bytes, bitsPerPixel);

                File.WriteAllBytes(filename, bmp.GetBmpBytes(flipped: true));
            }
            else
            {
                throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectFileFormat", this.Name));
            }
        }
    }
}