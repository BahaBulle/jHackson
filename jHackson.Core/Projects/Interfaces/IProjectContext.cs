using jHackson.Core.Table;
using System.Collections.Generic;
using System.IO;

namespace jHackson.Core.Projects
{
    public interface IProjectContext
    {
        void AddBuffer(int id, object obj);

        void AddTable(int id, ITable value);

        void AddVariable(string name, string value);

        bool BufferExists(int id);

        object GetBuffer(int id, bool forceCreation = false);

        MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false);

        ITable GetTable(int id);

        string GetVariable(string name);

        Dictionary<string, string> GetVariables();

        bool TableExists(int id);
    }
}