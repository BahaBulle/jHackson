// <copyright file="IProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.IO;
    using JHackson.Core.Tables;
    using JHackson.Core.Variables;

    public interface IProjectContext
    {
        /// <summary>
        /// Gets the list of tables loaded for the project.
        /// </summary>
        TablesDictionary Tables { get; }

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
    }
}