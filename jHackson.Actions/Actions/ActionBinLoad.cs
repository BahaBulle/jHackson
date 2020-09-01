// <copyright file="ActionBinLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Binary
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    /// <summary>
    /// Provides a class that allows loading a file in binary format.
    /// </summary>
    public class ActionBinLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBinLoad" /> class.
        /// </summary>
        public ActionBinLoad()
        {
            this.Name = "BinLoad";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.To = null;
        }

        /// <summary>
        /// Gets or sets the file to load.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where load the file.
        /// </summary>
        public int? To { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
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

        /// <summary>
        /// Execute the process of this action.
        /// </summary>
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

            this.FileName = PluginsHelper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);
        }
    }
}