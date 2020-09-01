// <copyright file="ActionTableSave.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tables.Actions
{
    using System;
    using System.Globalization;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionTableSave : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionTableSave()
        {
            this.Name = "TableSave";
            this.Title = null;
            this.Todo = true;
        }

        public string FileName { get; set; }

        public int? Id { get; set; }

        public override void Check()
        {
            if (!this.Id.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.Id), this.Id.HasValue ? this.Id.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            var tbl = this.Context.GetTable(this.Id.Value);

            tbl.Save(this.FileName);
        }

        public override void Init(IProjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.FileName = PluginsHelper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);
        }
    }
}