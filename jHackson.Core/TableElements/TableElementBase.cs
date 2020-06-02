// <copyright file="TableElementBase.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.TableElements
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.TableElements.Extensions;

    public abstract class TableElementBase : ITableElement
    {
        public const string CHARENDBLOCK = "/";
        public const string CHARPARAMINSERT = "@";

        private byte[] keyBytes;
        private Regex rgxEndBlock;

        private char[] valueChars;

        public TableElementBase()
        {
            this.Errors = new List<string>();
            this.Warnings = new List<string>();
        }

        public List<string> Errors { get; }

        public bool HasErrors => this.Errors.Count > 0;

        public bool HasWarnings => this.Warnings.Count > 0;

        public char? Identifier { get; protected set; }

        public string Key { get; protected set; }

        public int KeySize { get; protected set; }

        public string Line { get; protected set; }

        public List<ITableElementParam> ListParam { get; protected set; }

        public string Name { get; protected set; }

        public int NbParam { get; protected set; }

        public Regex RegexLine { get; protected set; }

        public string RegexValue { get; protected set; }

        public string Value { get; protected set; }
        public int ValueSize { get; protected set; }

        public List<string> Warnings { get; }

        public bool WarningsAsErrors { get; protected set; }

        protected Regex RgxEndBlock
        {
            get
            {
                if (this.rgxEndBlock == null)
                {
                    this.rgxEndBlock = new Regex(@"^/([0-9A-Fa-f]+)=(.+)$");
                }

                return this.rgxEndBlock;
            }
        }

        protected Regex RgxParamInsert => new Regex(@"^@=(.+)$");

        public byte[] GetKeyBytes()
        {
            return this.keyBytes;
        }

        public char[] GetValueChars()
        {
            return this.valueChars;
        }

        public void Init()
        {
            this.Split();
        }

        public virtual bool IsThisElement(string line)
        {
            return this.RegexLine.IsMatch(line);
        }

        public ITableElement WithLine(string line)
        {
            this.Line = line;

            return this;
        }

        public ITableElement WithWarningsAsErrors(bool value)
        {
            this.WarningsAsErrors = value;

            return this;
        }

        protected void AddError(string message)
        {
            if (this.WarningsAsErrors)
            {
                this.Errors.Add(message);
            }
            else
            {
                this.Warnings.Add(message);
            }
        }

        protected void AddWarning(string message)
        {
            this.Warnings.Add(message);
        }

        protected virtual void SetKey(string key)
        {
            this.Key = key;

            if (this.Key != null && this.Key.Length % 2 != 0)
            {
                throw new JHacksonTableException(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", LocalizationManager.GetMessage("core.tableElement.incorrectKeyLength", this.Line)));
            }

            this.KeySize = this.Key.Length / 2;
            this.keyBytes = new byte[this.KeySize];

            var tab = this.Key.SplitByLength(2);

            int cpt = 0;
            foreach (string s in tab)
            {
                bool result = byte.TryParse(s, NumberStyles.HexNumber, null as IFormatProvider, out this.keyBytes[cpt++]);

                if (!result)
                {
                    throw new JHacksonTableException(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", LocalizationManager.GetMessage("core.tableElement.incorrectKeySyntax", this.Line)));
                }
            }
        }

        protected virtual void SetParam()
        {
        }

        protected virtual void SetRegexValue()
        {
            string[] badChars = { "\\", ".", "$", "^", "{", "[", "(", "|", ")", "*", "+", "?" };
            string[] goodChars = { "\\\\", "\\.", "\\$", "\\^", "\\{", "\\[", "\\(", "\\|", "\\)", "\\*", "\\+", "\\?" };

            this.RegexValue = this.Value;
            for (int i = 0; i < badChars.Length; i++)
            {
                this.RegexValue = this.RegexValue.Replace(badChars[i], goodChars[i]);
            }
        }

        protected virtual void SetValue(string value)
        {
            if (value == null)
            {
                this.Value = value;
                this.valueChars = Array.Empty<char>();
                this.ValueSize = 0;
            }
            else
            {
                this.Value = value
                    .Replace("\\n", "\n", StringComparison.InvariantCulture)
                    .Replace("\\r", "\r", StringComparison.InvariantCulture)
                    .Replace("\\t", "\t", StringComparison.InvariantCulture);

                this.valueChars = this.Value.ToCharArray();
                this.ValueSize = this.valueChars.Length;
            }
        }

        protected abstract void Split();
    }
}