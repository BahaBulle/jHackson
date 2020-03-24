using jHackson.Core.Actions;
using jHackson.Core.Exceptions;
using jHackson.Core.Json.JsonConverters;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace jHackson.Core.Projects
{
    public class ProjectJson : IProjectJson
    {
        #region Constantes

        private const string VERSION = "0.9";

        #endregion

        #region Fields

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IProjectContext _context;
        private readonly Regex _regex = new Regex(@"^jHackson v(\d{1,2}\.\d{1,2}) ©BahaBulle$");

        #endregion

        #region Properties

        [JsonConverter(typeof(ActionJsonConverter))]
        public List<IActionJson> Actions { get; set; }

        public string Application { get; set; }

        public string Console { get; set; }

        public string Description { get; set; }

        public string Game { get; set; }

        [JsonConverter(typeof(VariableJsonConverter))]
        public List<IActionVariable> Variables { get; set; }

        public string Version { get; set; }

        #endregion

        #region Constructors

        public ProjectJson()
        {
            this._context = new ProjectContext();
        }

        #endregion

        #region Publics methods

        public void Check()
        {
            var hasErrors = false;

            hasErrors = this.ControlApplicationVersion();

            foreach (var action in this.Actions)
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
                throw new JHacksonException("There are errors in the script!");
        }

        public void Execute()
        {
            foreach (var action in this.Actions)
            {
                action.Execute();

                if (action.HasErrors)
                {
                    action.GetErrors()
                        .ForEach(x => _logger.Error(x));

                    LogManager.Flush();

                    throw new JHacksonException("There are errors in the script!");
                }
            }
        }

        public void Init()
        {
            foreach (var variable in this.Variables)
            {
                this._context.AddVariable(variable.Name, variable.Value);
            }

            foreach (var action in this.Actions)
            {
                action.Init(this._context);
            }
        }

        #endregion

        #region Private methods

        private bool ControlApplicationVersion()
        {
            var result = this._regex.Match(this.Application);

            if (!result.Success)
            {
                _logger.Error($"The json file is incorrect!");
                return true;
            }

            if (result.Groups[1].Value != VERSION)
            {
                _logger.Error($"The version of the json script ({this.Version}) are not supported by this version of application ({VERSION})");
                return true;
            }

            return false;
        }

        #endregion
    }
}