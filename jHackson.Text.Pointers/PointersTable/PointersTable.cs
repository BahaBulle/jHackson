// <copyright file="PointersTable.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers
{
    using System.Collections.Generic;
    using JHackson.Core.PointersTable;

    /// <summary>
    /// Provides a class that allows to manage pointers.
    /// </summary>
    public class PointersTable : IPointersTable
    {
        private readonly List<ITextPointer> listPointers;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointersTable"/> class.
        /// </summary>
        public PointersTable()
        {
            this.listPointers = new List<ITextPointer>();
        }
    }
}