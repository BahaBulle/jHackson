// <copyright file="IActionJson.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    using System.Collections.Generic;
    using JHackson.Core.Projects;

    public interface IActionJson
    {
        bool HasErrors { get; }

        string Name { get; }

        string Title { get; set; }

        bool? Todo { get; set; }

        void AddError(string message);

        void Check();

        void Execute();

        List<string> GetErrors();

        void Init(IProjectContext context);
    }
}