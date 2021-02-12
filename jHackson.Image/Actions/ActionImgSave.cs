// <copyright file="ActionImgSave.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image.Actions
{
    using System;
    using System.Globalization;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    /// <summary>
    /// Provides a action which allows to save a MemoryStream in a file in different format.
    /// </summary>
    public class ActionImgSave : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionImgSave" /> class.
        /// </summary>
        public ActionImgSave()
        {
            this.Name = "ImgSave";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Format = null;
            this.From = null;
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
        public ImagePattern ImageParameters { get; set; }

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

            if (this.ImageParameters == null)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.ImageParameters), "null"));
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

            msSource.Position = 0;

            var imageFormat = ImageHelper.GetImageFormat(this.ImageParameters.Format);

            if (imageFormat == null)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectImageFormat"));
            }

            var image = imageFormat.Convert(msSource, this.ImageParameters);

            format.Save(this.FileName, image);
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