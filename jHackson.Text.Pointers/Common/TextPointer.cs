// <copyright file="TextPointer.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers
{
    using JHackson.Core;
    using JHackson.Core.PointersTable;

    /// <summary>
    /// Provides a class to manage a TextPointer.
    /// </summary>
    public class TextPointer : ITextPointer
    {
        /// <summary>
        ///  Gets or sets the adress of the Pointer.
        /// </summary>
        public long Adress { get; set; }

        /// <summary>
        /// Gets or sets the calculation of the Pointer.
        /// </summary>
        public string Calculation { get; set; }

        /// <summary>
        ///  Gets or sets the endian type of the Pointer.
        /// </summary>
        public EnumEndianType Endian { get; set; }

        /// <summary>
        /// Gets or sets the id of the Pointer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes of the Pointer.
        /// </summary>
        public short NumberOfBytes { get; set; }

        /// <summary>
        /// Gets or sets the value of the Pointer.
        /// </summary>
        public ulong Value { get; set; }
    }
}