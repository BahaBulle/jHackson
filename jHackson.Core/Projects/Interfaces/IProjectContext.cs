// <copyright file="IProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using JHackson.Core.Buffers;
    using JHackson.Core.Tables;
    using JHackson.Core.Variables;

    /// <summary>
    /// Provides a class to stock loading datas in the project context.
    /// </summary>
    public interface IProjectContext
    {
        /// <summary>
        /// Gets the list of buffers of the project.
        /// </summary>
        BuffersDictionary Buffers { get; }

        /// <summary>
        /// Gets the list of tables loaded for the project.
        /// </summary>
        TablesDictionary Tables { get; }

        /// <summary>
        /// Gets the list of variables of the project.
        /// </summary>
        VariablesDictionary Variables { get; }
    }
}