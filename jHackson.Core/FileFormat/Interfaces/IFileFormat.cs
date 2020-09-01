// <copyright file="IFileFormat.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.FileFormat
{
    /// <summary>
    /// Interface for file format.
    /// </summary>
    public interface IFileFormat
    {
        /// <summary>
        /// Gets the name of the file format.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Save data source in a file.
        /// </summary>
        /// <param name="filename">Filename where save data source.</param>
        /// <param name="buffer">Data source to save.</param>
        void Save(string filename, object buffer);
    }
}