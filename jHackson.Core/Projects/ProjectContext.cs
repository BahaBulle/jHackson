// <copyright file="ProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Variables;
    using JHackson.Text.Tables;

    public class ProjectContext : IProjectContext
    {
        private readonly Dictionary<int, object> listBuffers = new Dictionary<int, object>();

        private readonly Dictionary<string, ITable> listTables = new Dictionary<string, ITable>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class.
        /// </summary>
        public ProjectContext()
        {
            this.Variables = new VariablesDictionary();
        }

        /// <summary>
        /// Gets the list of variables of the project.
        /// </summary>
        public VariablesDictionary Variables { get; private set; }

        /// <summary>
        /// Add a buffer in the context.
        /// </summary>
        /// <param name="id">Id of the buffer.</param>
        /// <param name="obj">Buffer.</param>
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

        /// <summary>
        /// Add a table in the context.
        /// </summary>
        /// <param name="name">Name of the table.</param>
        /// <param name="value">Table.</param>
        public void AddTable(string name, ITable value)
        {
            if (this.listTables.ContainsKey(name))
            {
                this.listTables[name] = null;
                this.listTables.Remove(name);
            }

            this.listTables.Add(name, value);
        }

        /// <summary>
        /// Test existence of a buffer.
        /// </summary>
        /// <param name="id">Id of the buffer to test existence.</param>
        /// <returns>True if the buffer exists, false otherwise.</returns>
        public bool BufferExists(int id)
        {
            return this.listBuffers.ContainsKey(id);
        }

        /// <summary>
        /// Get a specific buffer.
        /// </summary>
        /// <param name="id">Id of the buffer to get.</param>
        /// <param name="forceCreation">Force creation of the buffer if not exists.</param>
        /// <returns>The buffer.</returns>
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

        /// <summary>
        /// Get a specific buffer as MemoryStream.
        /// </summary>
        /// <param name="id">Id of the buffer to get.</param>
        /// <param name="forceCreation">Force creation of the buffer if not exists.</param>
        /// <returns>The buffer as a MemoryStream.</returns>
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

        /// <summary>
        /// Get a specific ITable.
        /// </summary>
        /// <param name="name">Name of the table to get.</param>
        /// <returns>The ITable.</returns>
        public ITable GetTable(string name)
        {
            if (!this.listTables.ContainsKey(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableUnknow", name));
            }

            return this.listTables[name];
        }

        /// <summary>
        /// Test existence of a ITable.
        /// </summary>
        /// <param name="name">Name of the ITable to test existence.</param>
        /// <returns>True if the table exists, false otherwise.</returns>
        public bool TableExists(string name)
        {
            return this.listTables.ContainsKey(name);
        }
    }
}