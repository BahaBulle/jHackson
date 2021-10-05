// <copyright file="EnumPointersTableMode.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers
{
    /// <summary>
    /// Enum to indicate the insertion mode of pointers in the PointersTable.
    /// </summary>
    public enum EnumPointersTableMode
    {
        /// <summary>
        /// Indicate to create a new PointersTable.
        /// </summary>
        New,

        /// <summary>
        /// Indicate to add pointers to the existing PointersTable.
        /// </summary>
        Append,
    }
}