using jHackson.Core.Exceptions;
using jHackson.Core.Localization;
using jHackson.Core.Table;
using System.Collections.Generic;
using System.IO;

namespace jHackson.Core.Projects
{
    public class ProjectContext : IProjectContext
    {
        private readonly Dictionary<int, object> _listBuffers = new Dictionary<int, object>();
        private readonly Dictionary<int, ITable> _listTables = new Dictionary<int, ITable>();
        private readonly Dictionary<string, string> _listVariables = new Dictionary<string, string>();

        public void AddBuffer(int id, object obj)
        {
            if (this._listBuffers.ContainsKey(id))
            {
                if (this._listBuffers[id] is MemoryStream ms)
                {
                    ms.Close();
                    ms.Dispose();
                }
                this._listBuffers[id] = null;

                this._listBuffers.Remove(id);
            }

            this._listBuffers.Add(id, obj);
        }

        public void AddTable(int id, ITable value)
        {
            if (this._listTables.ContainsKey(id))
            {
                this._listTables[id] = null;
                this._listTables.Remove(id);
            }

            this._listTables.Add(id, value);
        }

        public void AddVariable(string name, string value)
        {
            if (this._listVariables.ContainsKey(name))
                this._listVariables[name] = value;
            else
                this._listVariables.Add(name, value);
        }

        public bool BufferExists(int id)
        {
            return this._listBuffers.ContainsKey(id);
        }

        public object GetBuffer(int id, bool forceCreation = false)
        {
            if (!this._listBuffers.ContainsKey(id) && forceCreation)
                this.AddBuffer(id, new MemoryStream());
            else if (!this._listBuffers.ContainsKey(id))
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", id));

            return this._listBuffers[id];
        }

        public MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false)
        {
            if (!this._listBuffers.ContainsKey(id) && forceCreation)
                this.AddBuffer(id, new MemoryStream());

            if (!this._listBuffers.ContainsKey(id))
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", id));

            if (this._listBuffers.ContainsKey(id) && !(this._listBuffers[id] is MemoryStream))
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferNotMemoryStream", id));

            return this._listBuffers[id] as MemoryStream;
        }

        public ITable GetTable(int id)
        {
            if (!this._listTables.ContainsKey(id))
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableUnknow", id));

            return this._listTables[id];
        }

        public string GetVariable(string name)
        {
            if (!this._listVariables.ContainsKey(name))
                throw new JHacksonException(LocalizationManager.GetMessage("core.variableUnknow", name));

            return this._listVariables[name];
        }

        public Dictionary<string, string> GetVariables()
        {
            return this._listVariables;
        }

        public bool TableExists(int id)
        {
            return this._listTables.ContainsKey(id);
        }
    }
}