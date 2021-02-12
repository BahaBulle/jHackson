// <copyright file="ActionPngLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core.Actions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;
    using SkiaSharp;

    /// <summary>
    /// Provides a class that allows loading a file in png format.
    /// </summary>
    public class ActionPngLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionPngLoad" /> class.
        /// </summary>
        public ActionPngLoad()
        {
            this.Name = "PngLoad";
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
        /// Gets or sets the parameters of the image.
        /// </summary>
        public ImagePattern ImageParameters { get; set; }

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

            if (this.ImageParameters == null)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.ImageParameters), "null"));
            }
        }

        /// <summary>
        /// Execute the conversion of the image.
        /// </summary>
        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            using (var sourceStream = new MemoryStream(File.ReadAllBytes(this.FileName)))
            {
                var imageInfo = new SKImageInfo(this.ImageParameters.Width, this.ImageParameters.Height, SKColorType.Rgba8888);

                var image = SKBitmap.Decode(sourceStream, imageInfo);

                var imageFormat = ImageHelper.GetImageFormat(this.ImageParameters.Format);

                if (imageFormat == null)
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("formats.incorrectImageFormat"));
                }

                var binaryData = imageFormat.ConvertBack(image, this.ImageParameters);

                binaryData.Position = 0;
                this.Context.Buffers.Add(this.To.Value, binaryData);
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
        }
    }
}