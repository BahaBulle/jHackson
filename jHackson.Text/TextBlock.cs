// <copyright file="TextBlock.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text
{
    using System.Collections.Generic;
    using JHackson.Text.Pointers;
    using JHackson.Text.Tables;

    /// <summary>
    /// Provides a class that contains a bloc of text.
    /// </summary>
    public class TextBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock" /> class.
        /// </summary>
        public TextBlock()
        {
            this.Bytes = new List<byte>();
            this.Pointers = new List<TextPointer>();
        }

        /// <summary>
        /// Gets or sets the end adress of the TextBlock.
        /// </summary>
        public long AdressEnd { get; set; }

        /// <summary>
        /// Gets or sets the starting adress of the TextBlock.
        /// </summary>
        public long AdressStart { get; set; }

        /// <summary>
        /// Gets the bytes of the TextBlock.
        /// </summary>
        public List<byte> Bytes { get; private set; }

        /// <summary>
        ///  Gets the list of pointers of the TextBlock.
        /// </summary>
        public List<TextPointer> Pointers { get; private set; }

        /// <summary>
        /// Gets or sets the size of the TextBlock in bytes.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the table used to convert bytes to text or vice versa.
        /// </summary>
        public ITable Table { get; set; }

        /// <summary>
        /// Gets or Sets the text.
        /// </summary>
        public string Text { get; set; }
    }
}