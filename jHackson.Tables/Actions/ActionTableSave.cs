﻿using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Localization;
using jHackson.Core.Projects;
using NLog;

namespace jHackson.Tables.Actions
{
    public class ActionTableSave : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.Id), this.Id.HasValue ? this.Id.Value.ToString() : "null"));
        }

        public override void Execute()
        {
            if (this.Title != null)
                _logger.Info(this.Title);

            var tbl = this._context.GetTable(this.Id.Value);

            tbl.Save(this.FileName);
        }

        public override void Init(IProjectContext context)
        {
            this.FileName = Helper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);
        }
    }
}