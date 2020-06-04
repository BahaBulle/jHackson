// <copyright file="ProjectJson.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JHackson.Core.Actions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Json.JsonConverters;
    using JHackson.Core.Localization;
    using Newtonsoft.Json;
    using NLog;

    public class ProjectJson : IProjectJson
    {
        private const string VERSION = "0.9";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IProjectContext context;
        private readonly Regex regex = new Regex(@"^jHackson v(\d{1,2}\.\d{1,2}) ©BahaBulle$");

        public ProjectJson()
        {
            this.context = new ProjectContext();
        }

        [JsonConverter(typeof(ActionJsonConverter))]
        [JsonProperty]
        public List<IActionJson> Actions { get; private set; }

        public string Application { get; set; }

        public string Console { get; set; }

        public string Description { get; set; }

        public string Game { get; set; }

        [JsonConverter(typeof(VariableJsonConverter))]
        [JsonProperty]
        public List<IActionVariable> Variables { get; private set; }

        public string Version { get; set; }

        public void Check()
        {
            var hasErrors = false;

            hasErrors = this.ControlApplicationVersion();

            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Check();

                if (action.HasErrors)
                {
                    action.GetErrors()
                        .ForEach(x => Logger.Error(x));
                    hasErrors = true;
                }
            }

            LogManager.Flush();

            if (hasErrors)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.project.scriptErrors"));
            }
        }

        public void Execute()
        {
            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Execute();

                if (action.HasErrors)
                {
                    action.GetErrors()
                        .ForEach(x => Logger.Error(x));

                    LogManager.Flush();

                    throw new JHacksonException(LocalizationManager.GetMessage("core.project.scriptErrors"));
                }
            }
        }

        public void Init()
        {
            foreach (var variable in this.Variables)
            {
                this.context.AddVariable(variable.Name, variable.Value);
            }

            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Init(this.context);
            }
        }

        private bool ControlApplicationVersion()
        {
            var result = this.regex.Match(this.Application);

            if (!result.Success)
            {
                Logger.Error(LocalizationManager.GetMessage("core.project.incorrectProjectFile"));
                return true;
            }

            if (result.Groups[1].Value != VERSION)
            {
                Logger.Error(LocalizationManager.GetMessage("core.project.incorrectVersion", this.Version, VERSION));
                return true;
            }

            return false;
        }
    }
}