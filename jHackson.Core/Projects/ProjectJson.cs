using jHackson.Core.Actions;
using jHackson.Core.Exceptions;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Localization;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace jHackson.Core.Projects
{
    public class ProjectJson : IProjectJson
    {
        private const string VERSION = "0.9";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IProjectContext _context;
        private readonly Regex _regex = new Regex(@"^jHackson v(\d{1,2}\.\d{1,2}) ©BahaBulle$");

        public ProjectJson()
        {
            this._context = new ProjectContext();
        }

        [JsonConverter(typeof(ActionJsonConverter))]
        public List<IActionJson> Actions { get; set; }

        public string Application { get; set; }

        public string Console { get; set; }

        public string Description { get; set; }

        public string Game { get; set; }

        [JsonConverter(typeof(VariableJsonConverter))]
        public List<IActionVariable> Variables { get; set; }

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
                        .ForEach(x => _logger.Error(x));
                    hasErrors = true;
                }
            }

            LogManager.Flush();

            if (hasErrors)
                throw new JHacksonException(LocalizationManager.GetMessage("core.project.scriptErrors"));
        }

        public void Execute()
        {
            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Execute();

                if (action.HasErrors)
                {
                    action.GetErrors()
                        .ForEach(x => _logger.Error(x));

                    LogManager.Flush();

                    throw new JHacksonException(LocalizationManager.GetMessage("core.project.scriptErrors"));
                }
            }
        }

        public void Init()
        {
            foreach (var variable in this.Variables)
            {
                this._context.AddVariable(variable.Name, variable.Value);
            }

            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Init(this._context);
            }
        }

        private bool ControlApplicationVersion()
        {
            var result = this._regex.Match(this.Application);

            if (!result.Success)
            {
                _logger.Error(LocalizationManager.GetMessage("core.project.incorrectProjectFile"));
                return true;
            }

            if (result.Groups[1].Value != VERSION)
            {
                _logger.Error(LocalizationManager.GetMessage("core.project.incorrectVersion", this.Version, VERSION));
                return true;
            }

            return false;
        }
    }
}