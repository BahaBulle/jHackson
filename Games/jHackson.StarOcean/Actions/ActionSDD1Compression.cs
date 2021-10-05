// <copyright file="ActionSDD1Compression.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.Actions
{
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.StarOcean.SDD1Algorithm;
    using NLog;

    /// <summary>
    /// Provides a action which allows to compress data using SDD1 compression like Star Ocean on Super Famicom.
    /// </summary>
    public class ActionSDD1Compression : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSDD1Compression"/> class.
        /// </summary>
        public ActionSDD1Compression()
            : base()
        {
            this.Name = "SNES-SO-SDD1-C";
            this.Title = null;
            this.Todo = true;

            this.From = null;
            this.To = null;
        }

        /// <summary>
        /// Gets or sets the id of the MemoryStream to read.
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where load the file.
        /// </summary>
        public int? To { get; set; }

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

            ISdd1Comp sdd1 = new Sdd1();

            var msDestination = sdd1.Compress(msSource);

            this.Context.Buffers.Add(this.To.Value, msDestination);
        }
    }
}