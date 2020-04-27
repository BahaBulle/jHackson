using jHackson.Core.Actions;
using jHackson.Core.Json.JsonConverters;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;

namespace jHackson.Actions.Actions
{
    public class ActionGroup : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionGroup()
        {
            this.Name = "Group";
            this.Title = null;
            this.Todo = true;
        }

        [JsonConverter(typeof(ActionJsonConverter))]
        public List<IActionJson> Actions { get; set; }

        public override void Check()
        {
            foreach (var action in this.Actions)
            {
                action.Check();

                if (action.HasErrors)
                    this.AddErrors(action.GetErrors());
            }
        }

        public override void Execute()
        {
            if (this.Todo.HasValue && this.Todo.Value)
            {
                if (this.Title != null)
                    _logger.Info(this.Title);

                foreach (var action in this.Actions)
                {
                    action.Execute();
                }
            }
        }
    }
}