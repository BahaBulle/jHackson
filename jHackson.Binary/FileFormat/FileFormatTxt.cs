// <copyright file="FileFormatTxt.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary.FileFormat
{
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.FileFormat;
    using JHackson.Core.Localization;

    public class FileFormatTxt : IFileFormat
    {
        public FileFormatTxt()
        {
            this.Name = "Txt";
        }

        public string Name { get; set; }

        public void Save(string filename, object buffer)
        {
            if (buffer is MemoryStream ms)
            {
                string directory = Path.GetDirectoryName(filename);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllBytes(filename, ms.ToArray());
            }
            else
            {
                throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectFileFormat", this.Name));
            }
        }
    }
}
