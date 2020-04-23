using jHackson.Core.Exceptions;
using jHackson.Core.Localization;
using jHackson.Core.Table;
using jHackson.Tables.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jHackson.Tables
{
    public class Table : ITable
    {
        public const string LabelTableAscii = "ascii";

        private readonly Encoding _encode;
        private readonly TableValueCollection _valueCollection;

        public Table(string encoding = null) : this()
        {
            if (encoding == null)
                encoding = string.Empty;

            switch (encoding.ToUpper())
            {
                case "ASCII":
                    this._encode = Encoding.ASCII;
                    break;

                case "UTF8":
                    this._encode = Encoding.UTF8;
                    break;

                case "UTF16":
                case "UNICODE":
                    this._encode = Encoding.Unicode;
                    break;

                case "UTF32":
                    this._encode = Encoding.UTF32;
                    break;

                default:
                    this._encode = Encoding.Default;
                    break;
            }
        }

        public Table(Encoding encoding) : this()
        {
            this._encode = encoding;
        }

        private Table()
        {
            this._valueCollection = new TableValueCollection();
        }

        public int Count => this._valueCollection.Count;
        public string Name { get; private set; }
        public bool WarningAsErrors { get; set; }

        public void Load(List<string> list)
        {
            foreach (var line in list)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                this.Add(line);
            }
        }

        /// <summary>
        /// Load a tbl file in memory
        /// </summary>
        /// <param name="filename">Filename of the tbl file</param>
        /// <returns>Return True is success, false othrewise</returns>
        public void Load(string filename)
        {
            if (!File.Exists(filename))
                throw new jHacksonTableException(LocalizationManager.GetMessage("tables.unkwownFile", filename));

            this.Name = filename;

            using (var reader = new StreamReader(filename, this._encode))
            {
                this.Load(reader);
            }
        }

        /// <summary>
        /// Load a tbl file in memory
        /// </summary>
        /// <param name="reader">Reader of the tbl file</param>
        /// <returns>Return True is success, false othrewise</returns>
        public void Load(StreamReader reader)
        {
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                this.Add(line);
            }
        }

        public bool LoadStdAscii(bool? extend)
        {
            this.Name = "ascii";

            this.Add(@"0A=\n");

            for (int i = 0x20; i < 0x80; i++)
                Add(i.ToString("X") + "=" + Convert.ToChar(i));

            if (extend.HasValue && extend.Value)
            {
                for (int i = 0x80; i < 0x100; i++)
                    Add(i.ToString("X") + "=" + Convert.ToChar(i));
            }

            return true;
        }

        public void Save(string filename = null)
        {
            if (string.IsNullOrWhiteSpace(filename))
                this.Print();
            else
                this.SaveToFile(filename);
        }

        private void Add(string line)
        {
            var element = new TableElementFactory(line)
                .Build(this.WarningAsErrors);

            if (element != null)
            {
                if (!this._valueCollection.Contains(element))
                {
                    if (!element.HasErrors)
                        this._valueCollection.Add(element);
                }
            }
            else
            {
                var ex = new jHacksonTableException("");

                if (element.HasErrors)
                    ex.Data.Add("errors", element.Errors);

                if (this.WarningAsErrors && element.HasWarnings)
                    ex.Data.Add("warnings", element.Warnings);

                throw ex;
            }
        }

        private void Print()
        {
            foreach (var element in this._valueCollection)
            {
                Console.WriteLine(element.Line);
            }
        }

        private void SaveToFile(string filename)
        {
            using (var file = new StreamWriter(filename, false, this._encode))
            {
                foreach (var element in this._valueCollection)
                {
                    file.WriteLine(element.Line);
                }
            }
        }
    }
}