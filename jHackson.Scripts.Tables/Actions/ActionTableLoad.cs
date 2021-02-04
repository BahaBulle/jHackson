﻿// <copyright file="ActionTableLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Scripts.Tables
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionTableLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionTableLoad()
        {
            this.Name = "TableLoad";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Id = null;
            this.Extend = null;
        }

        public bool? Extend { get; set; }

        public string FileName { get; set; }

        public int? Id { get; set; }

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName) || (this.FileName.ToUpper(CultureInfo.InvariantCulture) != Table.LabelTableAscii && !File.Exists(this.FileName)))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", this.FileName, this.FileName ?? "null"));
            }

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

            var tbl = new Table();

            if (this.FileName.ToUpper(CultureInfo.InvariantCulture) == Table.LabelTableAscii)
            {
                tbl.LoadStdAscii(this.Extend);
            }
            else
            {
                tbl.Load(this.FileName);
            }

            this.Context.AddTable(this.Id.Value, tbl);
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