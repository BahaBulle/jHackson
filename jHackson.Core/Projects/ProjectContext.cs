using jHackson.Core.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace jHackson.Core.Projects
{
    public class ProjectContext : IProjectContext
    {
        #region Properties

        private readonly Dictionary<int, object> _listBuffers = new Dictionary<int, object>();
        private readonly Dictionary<int, object> _listTables = new Dictionary<int, object>();
        private readonly Dictionary<string, string> _listVariables = new Dictionary<string, string>();

        #endregion

        #region Methods for buffers

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

        public object GetBuffer(int id, bool forceCreation = false)
        {
            if (!this._listBuffers.ContainsKey(id) && forceCreation)
                this.AddBuffer(id, new MemoryStream());
            else if (!this._listBuffers.ContainsKey(id))
                throw new JHacksonException($"Buffer {id} doesn't not exist!");

            return this._listBuffers[id];
        }

        public MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false)
        {
            if (!this._listBuffers.ContainsKey(id) && forceCreation)
                this.AddBuffer(id, new MemoryStream());

            if (!this._listBuffers.ContainsKey(id))
                throw new JHacksonException(string.Format("Buffer {0} doesn't not exist!", id));

            if (this._listBuffers.ContainsKey(id) && !(this._listBuffers[id] is MemoryStream))
                throw new JHacksonException(string.Format("Buffer {0} is not a MemoryBuffer", id));

            return this._listBuffers[id] as MemoryStream;
        }

        #endregion

        #region Méthodes for Variables

        public void AddVariable(string name, string value)
        {
            if (this._listVariables.ContainsKey(name))
                this._listVariables[name] = value;
            else
                this._listVariables.Add(name, value);
        }

        public string GetVariable(string name)
        {
            if (!this._listVariables.ContainsKey(name))
                throw new JHacksonException($"Variable {name} doesn't not exist!");

            return this._listVariables[name];
        }

        public Dictionary<string, string> GetVariables()
        {
            return this._listVariables;
        }

        #endregion
    }
}