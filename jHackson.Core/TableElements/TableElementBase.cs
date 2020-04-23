using jHackson.Core.Exceptions;
using jHackson.Core.Localization;
using jHackson.Core.TableElements.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace jHackson.Core.TableElements
{
    public abstract class TableElementBase : ITableElement
    {
        public const string CHAR_END_BLOCK = "/";
        public const string CHAR_PARAM_INSERT = "@";

        protected readonly Regex rgxEndBlock = new Regex(@"^/([0-9A-Fa-f]+)=(.+)$");
        protected readonly Regex rgxParamInsert = new Regex(@"^@=(.+)$");

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
        public byte[] KeyBytes { get; protected set; }
        public int KeySize { get; protected set; }
        public string Line { get; protected set; }
        public List<ITableElementParam> ListParam { get; protected set; }
        public string Name { get; protected set; }
        public int NbParam { get; protected set; }
        public Regex RegexLine { get; protected set; }
        public string RegexValue { get; protected set; }
        public string Value { get; protected set; }
        public char[] ValueChars { get; protected set; }
        public int ValueSize { get; protected set; }
        public List<string> Warnings { get; }
        public bool WarningsAsErrors { get; protected set; }

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
                this.Errors.Add(message);
            else
                this.Warnings.Add(message);
        }

        protected void AddWarning(string message)
        {
            this.Warnings.Add(message);
        }

        protected virtual void SetKey(string key)
        {
            this.Key = key;

            if (this.Key != null && this.Key.Length % 2 != 0)
                throw new jHacksonTableException(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", LocalizationManager.GetMessage("core.tableElement.incorrectKeyLength", this.Line)));

            this.KeySize = this.Key.Length / 2;
            this.KeyBytes = new byte[this.KeySize];

            var tab = this.Key.SplitByLength(2);

            int cpt = 0;
            foreach (string s in tab)
            {
                bool result = byte.TryParse(s, NumberStyles.HexNumber, null as IFormatProvider, out this.KeyBytes[cpt++]);

                if (!result)
                    throw new jHacksonTableException(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", LocalizationManager.GetMessage("core.tableElement.incorrectKeySyntax", this.Line)));
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
                this.RegexValue = this.RegexValue.Replace(badChars[i], goodChars[i]);
        }

        protected virtual void SetValue(string value)
        {
            this.Value = value;

            this.Value = this.Value.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            this.ValueChars = this.Value.ToCharArray();
            this.ValueSize = this.ValueChars.Length;
        }

        protected abstract void Split();
    }
}