// <copyright file="ITable.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Tables
{
    using System.Collections.Generic;
    using System.IO;

    public interface ITable
    {
        int Count { get; }

        string Name { get; }

        bool WarningAsErrors { get; set; }

        void Load(string filename);

        void Load(StreamReader reader);

        void Load(List<string> list);

        bool LoadStdAscii(bool? extend);

        void Save(string filename = null);
    }
}