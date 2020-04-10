using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace jHackson.Core.TableElements
{
    public interface ITableElement
    {
        List<string> Errors { get; }
        bool HasErrors { get; }
        bool HasWarnings { get; }
        char? Identifier { get; }
        string Key { get; }
        byte[] KeyBytes { get; }
        int KeySize { get; }
        string Line { get; }
        List<ITableElementParam> ListParam { get; }
        string Name { get; }
        int NbParam { get; }
        Regex RegexLine { get; }
        string RegexValue { get; }
        string Value { get; }
        char[] ValueChars { get; }
        int ValueSize { get; }
        List<string> Warnings { get; }
        bool WarningsAsErrors { get; }

        void Init();

        bool IsThisElement(string line);

        ITableElement WithLine(string line);

        ITableElement WithWarningsAsErrors(bool value);
    }
}