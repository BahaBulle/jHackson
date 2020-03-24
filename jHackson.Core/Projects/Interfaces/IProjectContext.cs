using System.Collections.Generic;
using System.IO;

namespace jHackson.Core.Projects
{
    public interface IProjectContext
    {
        #region Methods for Buffers

        void AddBuffer(int id, object obj);

        object GetBuffer(int id, bool forceCreation = false);

        MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false);

        #endregion

        #region Methods for Variables

        void AddVariable(string name, string value);

        string GetVariable(string name);

        Dictionary<string, string> GetVariables();

        #endregion
    }
}