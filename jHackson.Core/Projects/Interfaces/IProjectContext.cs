// <copyright file="IProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.IO;
    using JHackson.Core.Variables;
    using JHackson.Text.Tables;

    public interface IProjectContext
    {
        /// <summary>
        /// Gets the list of variables of the project.
        /// </summary>
        VariablesDictionary Variables { get; }

        /// <summary>
        /// Add a buffer in the context.
        /// </summary>
        /// <param name="id">Id of the buffer.</param>
        /// <param name="obj">Buffer.</param>
        void AddBuffer(int id, object obj);

        /// <summary>
        /// Add a table in the context.
        /// </summary>
        /// <param name="name">Name of the table.</param>
        /// <param name="value">Table.</param>
        void AddTable(string name, ITable value);

        /// <summary>
        /// Test existence of a buffer.
        /// </summary>
        /// <param name="id">Id of the buffer to test existence.</param>
        /// <returns>True if the buffer exists, false otherwise.</returns>
        bool BufferExists(int id);

        /// <summary>
        /// Get a specific buffer.
        /// </summary>
        /// <param name="id">Id of the buffer to get.</param>
        /// <param name="forceCreation">Force creation of the buffer if not exists.</param>
        /// <returns>The buffer.</returns>
        object GetBuffer(int id, bool forceCreation = false);

        /// <summary>
        /// Get a specific buffer as MemoryStream.
        /// </summary>
        /// <param name="id">Id of the buffer to get.</param>
        /// <param name="forceCreation">Force creation of the buffer if not exists.</param>
        /// <returns>The buffer as a MemoryStream.</returns>
        MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false);

        /// <summary>
        /// Get a specific ITable.
        /// </summary>
        /// <param name="name">Name of the table to get.</param>
        /// <returns>The ITable.</returns>
        ITable GetTable(string name);

        /// <summary>
        /// Test existence of a ITable.
        /// </summary>
        /// <param name="name">Name of the ITable to test existence.</param>
        /// <returns>True if the table exists, false otherwise.</returns>
        bool TableExists(string name);
    }
}