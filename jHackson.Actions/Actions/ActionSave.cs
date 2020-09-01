// <copyright file="ActionSave.cs" company="BahaBulle">
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

    /// <summary>
    /// Provides a action which allows to save a MemoryStream in a file in different format.
    /// </summary>
    public class ActionSave : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSave" /> class.
        /// </summary>
        public ActionSave()
        {
            this.Name = "Save";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Format = null;
            this.From = null;
            this.Source = new BufferParameters();
        }

        /// <summary>
        /// Gets or sets the filename of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the id of the MemoryStream to read.
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the parameters of the image.
        /// </summary>
        public ImageParameters ImageParameters { get; set; }

        /// <summary>
        /// Gets or sets the MemoryStream source parameters.
        /// </summary>
        public BufferParameters Source { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
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

            if (!DataContext.FileFormatExists(this.Format))
            {
                this.AddError(LocalizationManager.GetMessage("core.fileFormatUnknow", this.Format));
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

            if (!this.Context.BufferExists(this.From.Value))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", this.From));
            }

            var msSource = this.Context.GetBufferMemoryStream(this.From.Value);
            var format = DataContext.GetFileFormat(this.Format);

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
                byte[] bytes = new byte[this.Source.Size.Value];
                msSource.Position = this.Source.AdressStart.Value;
                msSource.Read(bytes, 0, (int)this.Source.Size.Value);
                msSource.Position = this.Source.AdressStart.Value;

                msDest.Write(bytes, 0, (int)this.Source.Size.Value);

                if (this.ImageParameters != null)
                {
                    var imageFormat = DataContext.GetImageFormat(this.ImageParameters.Format);

                    if (imageFormat == null)
                    {
                        throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectImageFormat"));
                    }

                    var image = imageFormat.Convert(msDest, this.ImageParameters);

                    format.Save(this.FileName, image);
                }
                else
                {
                    format.Save(this.FileName, msDest);
                }
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

            this.FileName = PluginsHelper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);

            this.Source.Init();
        }
    }
}