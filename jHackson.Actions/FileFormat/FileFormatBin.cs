using jHackson.Core.Exceptions;
using jHackson.Core.FileFormat;
using System.IO;

namespace jHackson.Actions.FileFormat
{
    public class FileFormatBin : IFileFormat
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructors

        public FileFormatBin()
        {
            this.Name = "Bin";
        }

        #endregion

        #region Methods

        public void Save(string filename, object buffer)
        {
            if (buffer is MemoryStream ms)
            {
                var directory = Path.GetDirectoryName(filename);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllBytes(filename, ms.ToArray());
            }
            else
                throw new JHacksonException(string.Format("(EE) The buffer is not in the correct format {0}", this.Name));
        }

        #endregion
    }
}