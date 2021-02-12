// <copyright file="ProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Tables;
    using JHackson.Core.Variables;

    public class ProjectContext : IProjectContext
    {
        private readonly Dictionary<int, object> listBuffers = new Dictionary<int, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class.
        /// </summary>
        public ProjectContext()
        {
            this.Tables = new TablesDictionary();
            this.Variables = new VariablesDictionary();
        }

        /// <inheritdoc/>
        public TablesDictionary Tables { get; private set; }

        /// <inheritdoc/>
        public VariablesDictionary Variables { get; private set; }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool BufferExists(int id)
        {
            return this.listBuffers.ContainsKey(id);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
    }
}