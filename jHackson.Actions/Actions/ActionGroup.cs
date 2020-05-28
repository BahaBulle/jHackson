﻿// <copyright file="ActionGroup.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Actions
{
    using System.Collections.Generic;
    using System.Linq;
    using JHackson.Core.Actions;
    using JHackson.Core.Json.JsonConverters;
    using JHackson.Core.Projects;
    using Newtonsoft.Json;
    using NLog;

    public class ActionGroup : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
                {
                    this.AddErrors(action.GetErrors());
                }
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

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