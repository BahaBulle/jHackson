﻿// <copyright file="ITableElement.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.TableElements
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public interface ITableElement
    {
        List<string> Errors { get; }

        bool HasErrors { get; }

        bool HasWarnings { get; }

        char? Identifier { get; }

        string Key { get; }

        int KeySize { get; }

        string Line { get; }

        List<ITableElementParam> ListParam { get; }

        string Name { get; }

        int NbParam { get; }

        Regex RegexLine { get; }

        string RegexValue { get; }

        string Value { get; }

        int ValueSize { get; }

        List<string> Warnings { get; }

        bool WarningsAsErrors { get; }

        byte[] GetKeyBytes();

        char[] GetValueChars();

        void Init();

        bool IsThisElement(string line);

        ITableElement WithLine(string line);

        ITableElement WithWarningsAsErrors(bool value);
    }
}