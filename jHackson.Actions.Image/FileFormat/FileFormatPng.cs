// <copyright file="FileFormatPng.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Image.FileFormat
{
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.FileFormat;
    using JHackson.Core.Localization;
    using SkiaSharp;

    /// <summary>
    /// Provides a format which allows to save a MemoryStream in a PNG file.
    /// </summary>
    public class FileFormatPng : IFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFormatPng" /> class.
        /// </summary>
        public FileFormatPng()
        {
            this.Name = "Png";
        }

        /// <summary>
        /// Gets or sets the name of the format.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Save the MemoryStream into a PNG file.
        /// </summary>
        /// <param name="filename">Filename of the PNG file created.</param>
        /// <param name="buffer">MemoryStream to convert into PNG file.</param>
        public void Save(string filename, object buffer)
        {
            if (buffer is SKBitmap bitmap)
            {
                var directory = Path.GetDirectoryName(filename);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                using (var stream = File.OpenWrite(filename))
                {
                    data.SaveTo(stream);
                }
            }
            else
            {
                throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectFileFormat", this.Name));
            }
        }
    }
}