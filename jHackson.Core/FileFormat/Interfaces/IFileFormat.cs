namespace jHackson.Core.FileFormat
{
    public interface IFileFormat
    {
        string Name { get; }

        void Save(string filename, object buffer);
    }
}