using System.IO;

namespace jHackson.StarOcean.SDD1Algorithm
{
    public interface ISDD1Comp
    {
        MemoryStream Compress(MemoryStream in_buf);
    }
}