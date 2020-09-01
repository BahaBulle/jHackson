// <copyright file="ActionFileLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionFileLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionFileLoad()
        {
            this.Name = "FileLoad";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.To = null;
        }

        public string FileName { get; set; }

        public int? To { get; set; }

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName) || !File.Exists(this.FileName))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.FileName), this.FileName ?? "null"));
            }

            if (!this.To.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

#pragma warning disable CA2000 // Supprimer les objets avant la mise hors de portée
            var ms = new MemoryStream(File.ReadAllBytes(this.FileName));
#pragma warning restore CA2000 // Supprimer les objets avant la mise hors de portée
            this.Context.AddBuffer(this.To.Value, ms);
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