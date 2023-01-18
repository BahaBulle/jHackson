// <copyright file="ActionEvbExtract.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.Actions
{
    using System.Globalization;
    using System.IO;
    using jHackson.Okamiden.EVB;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using NLog;

    public class ActionEvbExtract : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionEvbExtract"/> class.
        /// </summary>
        public ActionEvbExtract()
        {
            this.Name = "EVB_EXTRACT";
            this.Title = null;
            this.Todo = true;

            this.From = null;
            this.To = null;
        }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
        public override void Check()
        {
            if (!this.From.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString(CultureInfo.InvariantCulture) : "null"));
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

            var msSource = this.Context.Buffers.Get<MemoryStream>(this.From.Value);

            var evbFile = new EvbFile(msSource);

            //var scriptFile = evbFile.ExtractScripts();

            //this.Context.Buffers.Add(this.To.Value, scriptFile);
        }

        /// <summary>
        /// Gets or sets the id of the MemoryStream to read.
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where load the file.
        /// </summary>
        public int? To { get; set; }
    }
}
