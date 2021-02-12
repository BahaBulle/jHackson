// <copyright file="ActionTableSave.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Tables
{
    using System;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionTableSave : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTableSave"/> class.
        /// </summary>
        public ActionTableSave()
        {
            this.Name = "TableSave";
            this.Title = null;
            this.Todo = true;
        }

        /// <summary>
        /// Gets or sets the alias (name) of the table.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the file to load.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.Alias))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.Alias), this.Alias ?? "null"));
            }
        }

        /// <summary>
        /// Execute the process of this action.
        /// </summary>
        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            var tbl = this.Context.Tables[this.Alias];

            tbl.Save(this.FileName);
        }

        /// <summary>
        /// Initialize this action.
        /// </summary>
        /// <param name="context">Context of the project.</param>
        public override void Init(IProjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.FileName = context.Variables.Replace(this.FileName);

            base.Init(context);
        }
    }
}