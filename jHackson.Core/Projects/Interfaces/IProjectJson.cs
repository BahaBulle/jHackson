using jHackson.Core.Actions;
using jHackson.Core.Json.JsonConverters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace jHackson.Core.Projects
{
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