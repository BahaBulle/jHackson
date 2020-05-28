// <copyright file="IProjectJson.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using JHackson.Core.Actions;
    using JHackson.Core.Json.JsonConverters;
    using Newtonsoft.Json;

    public interface IProjectJson
    {
        [JsonConverter(typeof(ActionJsonConverter))]
        List<IActionJson> Actions { get; set; }

        string Application { get; set; }

        string Console { get; set; }

        string Description { get; set; }

        string Game { get; set; }

        [JsonConverter(typeof(VariableJsonConverter))]
        List<IActionVariable> Variables { get; set; }

        string Version { get; set; }

        void Check();

        void Execute();

        void Init();
    }
}