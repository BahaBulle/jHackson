// <copyright file="ActionBinLoadCopy.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    public class ActionBinLoadCopy : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBinLoadCopy"/> class.
        /// </summary>
        public ActionBinLoadCopy()
            : base()
        {
            this.Name = "BinLoadCopy";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.To = null;
            this.Destination = new BufferParameters();
            this.Source = new BufferParameters();
        }

        public BufferParameters Destination { get; set; }

        /// <summary>
        /// Gets or sets the file to load.
        /// </summary>
        public string FileName { get; set; }

        public BufferParameters Source { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the MemoryStream where save data.
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

            using (var msSource = new MemoryStream(File.ReadAllBytes(this.FileName)))
            {
                var msDest = this.Context.Buffers.Get<MemoryStream>(this.To.Value, true);

                if (!this.Source.AdressStart.HasValue)
                {
                    this.Source.AdressStart = 0;
                }

                if (!this.Destination.AdressStart.HasValue)
                {
                    this.Destination.AdressStart = 0;
                }

                if (!this.Source.Size.HasValue && !this.Source.AdressEnd.HasValue)
                {
                    this.Source.Size = msSource.Length - this.Source.AdressStart;
                }

                if (!this.Destination.Size.HasValue && !this.Destination.AdressEnd.HasValue)
                {
                    this.Destination.Size = this.Source.Size;
                }

                if (this.Source.Size.Value > this.Destination.Size.Value)
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("actions.notEnoughSpace", this.Source.Size, this.Destination.Size));
                }

                var bytes = new byte[this.Source.Size.Value];
                msSource.Position = this.Source.AdressStart.Value;
                msSource.Read(bytes, 0, (int)this.Source.Size.Value);

                msDest.Position = this.Destination.AdressStart.Value;
                msDest.Write(bytes, 0, (int)this.Source.Size.Value);
            }
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

            this.Source.Init();
            this.Destination.Init();
        }
    }
}