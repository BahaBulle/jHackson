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
    using JHackson.Core.Variables;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    /// Provides a class that represent a project.
    /// </summary>
    public class ProjectJson : IProjectJson
    {
        private const string VERSION = "0.9";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IProjectContext context;

        private readonly Regex regex = new Regex(@"^jHackson v(\d{1,2}\.\d{1,2}) ©BahaBulle$");

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectJson"/> class.
        /// </summary>
        public ProjectJson()
        {
            this.context = new ProjectContext();
        }

        /// <summary>
        /// Gets the list of actions to execute.
        /// </summary>
        [JsonConverter(typeof(ActionJsonConverter))]
        [JsonProperty(Order = 4)]
        public List<IActionJson> Actions { get; private set; }

        /// <summary>
        /// Gets or sets the version of the application.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the console of the project.
        /// </summary>
        public string Console { get; set; }

        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the game of the project.
        /// </summary>
        public string Game { get; set; }

        /// <summary>
        /// Gets the list of plugins to load.
        /// </summary>
        [JsonConverter(typeof(PluginJsonConverter))]
        [JsonProperty(Order = 1)]
        public List<string> Plugins { get; private set; }

        /// <summary>
        /// Gets the list of tables to load.
        /// </summary>
        [JsonConverter(typeof(TableJsonConverter))]
        [JsonProperty(Order = 3)]
        public List<IActionTable> Tables { get; private set; }

        /// <summary>
        /// Gets the list of variables to load.
        /// </summary>
        [JsonProperty(Order = 2)]
        public List<Variable> Variables { get; private set; }

        /// <summary>
        /// Gets or sets the version of the project.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Check all possible errors.
        /// </summary>
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

        /// <summary>
        /// Execute all actions of the project.
        /// </summary>
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

        /// <summary>
        /// Initialise the project.
        /// </summary>
        public void Init()
        {
            foreach (var variable in this.Variables)
            {
                this.context.Variables.Add(variable.Name, variable.Value);
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