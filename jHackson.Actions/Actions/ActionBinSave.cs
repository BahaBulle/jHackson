// <copyright file="ActionBinSave.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionBinSave : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionBinSave()
        {
            this.Name = "BinSave";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Format = null;
            this.From = null;
            this.Source = new BufferParameters();
        }

        public string FileName { get; set; }

        public string Format { get; set; }

        public int? From { get; set; }

        public BufferParameters Source { get; set; }

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.FileName), this.FileName ?? "null"));
            }

            if (!this.From.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }

            if (string.IsNullOrWhiteSpace(this.Format))
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.Format), this.Format ?? "null"));
            }

            if (!DataContext.FormatExists(this.Format))
            {
                this.AddError(LocalizationManager.GetMessage("core.formatUnknow", this.Format));
            }
        }

        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            if (!this.Context.BufferExists(this.From.Value))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", this.From));
            }

            var msSource = this.Context.GetBufferMemoryStream(this.From.Value);
            var format = DataContext.GetFormat(this.Format);

            if (!this.Source.AdressStart.HasValue)
            {
                this.Source.AdressStart = 0;
            }

            if (!this.Source.Size.HasValue && !this.Source.AdressEnd.HasValue)
            {
                this.Source.Size = msSource.Length - this.Source.AdressStart;
            }

            using (var msDest = new MemoryStream())
            {
                var bytes = new byte[this.Source.Size.Value];
                msSource.Position = this.Source.AdressStart.Value;
                msSource.Read(bytes, 0, (int)this.Source.Size.Value);
                msSource.Position = this.Source.AdressStart.Value;

                msDest.Write(bytes, 0, (int)this.Source.Size.Value);

                format.Save(this.FileName, msDest);
            }
        }

        public override void Init(IProjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.FileName = Helper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);

            this.Source.Init();
        }
    }
}