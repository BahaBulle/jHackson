// <copyright file="ProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using JHackson.Core.Buffers;
    using JHackson.Core.PointersTable;
    using JHackson.Core.Tables;
    using JHackson.Core.Variables;

    /// <inheritdoc/>
    public class ProjectContext : IProjectContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class.
        /// </summary>
        public ProjectContext()
        {
            this.Buffers = new BuffersDictionary();
            this.PointersTables = new PointersTableDictionary();
            this.Tables = new TablesDictionary();
            this.Variables = new VariablesDictionary();
        }

        /// <inheritdoc/>
        public BuffersDictionary Buffers { get; private set; }

        /// <inheritdoc/>
        public PointersTableDictionary PointersTables { get; }

        /// <inheritdoc/>
        public TablesDictionary Tables { get; private set; }

        /// <inheritdoc/>
        public VariablesDictionary Variables { get; private set; }
    }
}