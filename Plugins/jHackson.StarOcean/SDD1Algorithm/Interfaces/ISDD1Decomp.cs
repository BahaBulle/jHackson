using System.IO;

namespace jHackson.StarOcean.SDD1Algorithm
{
    public interface ISDD1Decomp
    {
        MemoryStream Decompress(MemoryStream in_buf, ushort out_len);
    }
}