// <copyright file="ITable.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Tables
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents a table of element to convert text in binary or vice versa.
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Gets the number of elements in the Table.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the name of the Table.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether warnings are considered in errors.
        /// </summary>
        bool WarningAsErrors { get; set; }

        /// <summary>
        /// Load a table in memory from a file.
        /// </summary>
        /// <param name="filename">Fullpath of the file to load.</param>
        void Load(string filename);

        /// <summary>
        /// Load a table in memory from a stream.
        /// </summary>
        /// <param name="reader">Stream to load.</param>
        void Load(StreamReader reader);

        /// <summary>
        /// Load a table in memory from a list of elements.
        /// </summary>
        /// <param name="list">List of element to load.</param>
        void Load(List<string> list);

        /// <summary>
        /// Load a standard ASCII table.
        /// </summary>
        /// <param name="extend">Indicates if the table need to have extend ASCII characters.</param>
        void LoadStandardAscii(bool? extend);

        /// <summary>
        /// Save the content of the table in a file or in screen.
        /// </summary>
        /// <param name="filename">Fullpath of the file where save the table?.</param>
        void Save(string filename = null);
    }
}