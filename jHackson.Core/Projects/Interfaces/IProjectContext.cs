// <copyright file="IProjectContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Core.Table;

    public interface IProjectContext
    {
        void AddBuffer(int id, object obj);

        void AddTable(int id, ITable value);

        void AddVariable(string name, string value);

        bool BufferExists(int id);

        object GetBuffer(int id, bool forceCreation = false);

        MemoryStream GetBufferMemoryStream(int id, bool forceCreation = false);

        ITable GetTable(int id);

        string GetVariable(string name);

        Dictionary<string, string> GetVariables();

        bool TableExists(int id);
    }
}