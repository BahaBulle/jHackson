// <copyright file="ActionSDD1Compression.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.Actions
{
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.StarOcean.SDD1Algorithm;
    using NLog;

    public class ActionSDD1Compression : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionSDD1Compression()
        {
            this.Name = "SOSDD1Compression";
            this.Title = null;
            this.Todo = true;

            this.From = null;
            this.To = null;
        }

        public int? From { get; set; }

        public int? To { get; set; }

        public override void Check()
        {
            if (!this.From.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString() : "null"));
            }

            if (!this.To.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString() : "null"));
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            var msSource = this.Context.GetBufferMemoryStream(this.From.Value);

            ISDD1Comp sdd1 = new SDD1();

            var msDestination = sdd1.Compress(msSource);

            this.Context.AddBuffer(this.To.Value, msDestination);
        }
    }
}