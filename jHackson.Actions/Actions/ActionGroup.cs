using jHackson.Core.Actions;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Projects;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Check();

                if (action.HasErrors)
                    this.AddErrors(action.GetErrors());
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
                _logger.Info(this.Title);

            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Execute();
            }
        }

        public override void Init(IProjectContext context)
        {
            base.Init(context);

            foreach (var action in this.Actions.Where(x => x.Todo == true))
            {
                action.Init(context);
            }
        }
    }
}