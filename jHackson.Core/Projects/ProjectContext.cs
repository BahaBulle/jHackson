// <copyright file="ProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Scripts.Tables;

    public class ProjectContext : IProjectContext
    {
        private readonly Dictionary<int, object> listBuffers = new Dictionary<int, object>();

        private readonly Dictionary<int, ITable> listTables = new Dictionary<int, ITable>();

        private readonly Dictionary<string, string> listVariables = new Dictionary<string, string>();

        public void AddBuffer(int id, object obj)
        {
            if (this.listBuffers.ContainsKey(id))
            {
                if (this.listBuffers[id] is MemoryStream ms)
                {
                    ms.Close();
                    ms.Dispose();
                }

                this.listBuffers[id] = null;

                this.listBuffers.Remove(id);
            }

            this.listBuffers.Add(id, obj);
        }

        public void AddTable(int id, ITable value)
        {
            if (this.listTables.ContainsKey(id))
            {
                this.listTables[id] = null;
                this.listTables.Remove(id);
            }

            this.listTables.Add(id, value);
        }

        public void AddVariable(string name, string value)
        {
            if (this.listVariables.ContainsKey(name))
            {
                this.listVariables[name] = value;
            }
            else
            {
                this.listVariables.Add(name, value);
            }
        }

        public bool BufferExists(int id)
        {
            return this.listBuffers.ContainsKey(id);
        }

        public object GetBuffer(int id, bool forceCreation = false)
        {
            if (!this.listBuffers.ContainsKey(id) && forceCreation)
            {
                this.AddBuffer(id, new MemoryStream());
            }
            else if (!this.listBuffers.ContainsKey(id))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", id));
            }

            return this.listBuffers[id];
        }

        public MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false)
        {
            if (!this.listBuffers.ContainsKey(id) && forceCreation)
            {
                this.AddBuffer(id, new MemoryStream());
            }

            if (!this.listBuffers.ContainsKey(id))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", id));
            }

            if (this.listBuffers.ContainsKey(id) && !(this.listBuffers[id] is MemoryStream))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferNotMemoryStream", id));
            }

            return this.listBuffers[id] as MemoryStream;
        }

        public ITable GetTable(int id)
        {
            if (!this.listTables.ContainsKey(id))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableUnknow", id));
            }

            return this.listTables[id];
        }

        public string GetVariable(string name)
        {
            if (!this.listVariables.ContainsKey(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.variableUnknow", name));
            }

            return this.listVariables[name];
        }

        public Dictionary<string, string> GetVariables()
        {
            return this.listVariables;
        }

        public bool TableExists(int id)
        {
            return this.listTables.ContainsKey(id);
        }
    }
}