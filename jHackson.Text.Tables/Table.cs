// <copyright file="Table.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Tables
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;

    /// <summary>
    /// Represents a table of element to convert text in binary or vice versa.
    /// </summary>
    public class Table : ITable
    {
        public const string LABEL_TABLE_ASCII = "ascii";

        private readonly Encoding encode;

        private readonly TableValueCollection valueCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="encoding">Encoding of file.</param>
        public Table(string encoding = null)
            : this()
        {
            if (encoding == null)
            {
                encoding = string.Empty;
            }

            switch (encoding.ToUpper(CultureInfo.InvariantCulture))
            {
                case "ASCII":
                    this.encode = Encoding.ASCII;
                    break;

                case "UTF8":
                    this.encode = Encoding.UTF8;
                    break;

                case "UTF16":
                case "UNICODE":
                    this.encode = Encoding.Unicode;
                    break;

                case "UTF32":
                    this.encode = Encoding.UTF32;
                    break;

                default:
                    this.encode = Encoding.Default;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="encoding">Encoding of file.</param>
        public Table(Encoding encoding)
            : this()
        {
            this.encode = encoding;
        }

        private Table()
        {
            this.valueCollection = new TableValueCollection();
        }

        /// <inheritdoc/>
        public int Count => this.valueCollection.Count;

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public bool WarningAsErrors { get; set; }

        /// <inheritdoc/>
        public void Load(List<string> list)
        {
            if (list != null)
            {
                foreach (string line in list)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    this.Add(line);
                }
            }
        }

        /// <inheritdoc/>
        public void Load(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new JHacksonTableException(LocalizationManager.GetMessage("tables.unkwownFile", filename));
            }

            this.Name = filename;

            using (var reader = new StreamReader(filename, this.encode))
            {
                this.Load(reader);
            }
        }

        /// <inheritdoc/>
        public void Load(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                this.Add(line);
            }
        }

        /// <inheritdoc/>
        public void LoadStandardAscii(bool? extend)
        {
            this.Name = LABEL_TABLE_ASCII;

            this.Add(@"0A=\n");

            for (int i = 0x20; i < 0x80; i++)
            {
                this.Add(i.ToString("X", CultureInfo.InvariantCulture) + "=" + Convert.ToChar(i));
            }

            if (extend.HasValue && extend.Value)
            {
                for (int i = 0x80; i < 0x100; i++)
                {
                    this.Add(i.ToString("X", CultureInfo.InvariantCulture) + "=" + Convert.ToChar(i));
                }
            }
        }

        /// <inheritdoc/>
        public void Save(string filename = null)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                this.Print();
            }
            else
            {
                this.SaveToFile(filename);
            }
        }

        private void Add(string line)
        {
            var element = new TableElementFactory(line)
                .Build(this.WarningAsErrors);

            if (element != null)
            {
                if (!this.valueCollection.Contains(element))
                {
                    if (!element.HasErrors)
                    {
                        this.valueCollection.Add(element);
                    }
                }
            }
            else
            {
                var ex = new JHacksonTableException(string.Empty);

                if (element.HasErrors)
                {
                    ex.Data.Add("errors", element.Errors);
                }

                if (this.WarningAsErrors && element.HasWarnings)
                {
                    ex.Data.Add("warnings", element.Warnings);
                }

                throw ex;
            }
        }

        private void Print()
        {
            foreach (var element in this.valueCollection)
            {
                Console.WriteLine(element.Line);
            }
        }

        private void SaveToFile(string filename)
        {
            using (var file = new StreamWriter(filename, false, this.encode))
            {
                foreach (var element in this.valueCollection)
                {
                    file.WriteLine(element.Line);
                }
            }
        }
    }
}