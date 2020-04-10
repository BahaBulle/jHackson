using System.Collections.Generic;
using System.IO;

namespace jHackson.Core.Table
{
    public interface ITable
    {
        int Count { get; }
        string Name { get; }
        bool WarningAsErrors { get; set; }

        void Load(string filename);

        void Load(StreamReader reader);

        void Load(List<string> list);

        bool LoadStdAscii(bool? extend);

        void Print(string filename = null);
    }
}