// <copyright file="ActionTableLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Tables
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionTableLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTableLoad"/> class.
        /// </summary>
        public ActionTableLoad()
        {
            this.Alias = "TableLoad";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Alias = string.Empty;
            this.Extend = null;
        }

        /// <summary>
        /// Gets or sets the alias (name) of the table.
        /// </summary>
        public string Alias { get; set; }

        public bool? Extend { get; set; }

        /// <summary>
        /// Gets or sets the file to load.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName) || (this.FileName.ToUpper(CultureInfo.InvariantCulture) != Table.LabelTableAscii && !File.Exists(this.FileName)))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", this.FileName, this.FileName ?? "null"));
            }

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

            var tbl = new Table();

            if (this.FileName.ToUpper(CultureInfo.InvariantCulture) == Table.LabelTableAscii)
            {
                tbl.LoadStdAscii(this.Extend);
            }
            else
            {
                tbl.Load(this.FileName);
            }

            this.Context.Tables.Add(this.Alias, tbl);
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