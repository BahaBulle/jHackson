// <copyright file="TxtExtract.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Actions
{
    using System.Globalization;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using JHackson.Text.Pointers.Common;
    using NLog;

    /// <summary>
    /// Provides a action which allows to extract text from a stream.
    /// </summary>
    public class TxtExtract : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="TxtExtract" /> class.
        /// </summary>
        public TxtExtract()
            : base()
        {
            this.Name = "TxtExtract";
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
        /// Gets or sets parameters of text to extract.
        /// </summary>
        //public TxtParameters Text { get; set; }

        /// <summary>
        /// Gets or sets parameters of the pointers table.
        /// </summary>
        public PointerParameters Pointers { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the MemoryStream where save data.
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

            this.AddErrors(this.Pointers.Check(nameof(this.Pointers)));
        }

        /// <summary>
        /// Execute the process of this action.
        /// </summary>
        public override void Execute()
        {
        }

        /// <summary>
        /// Initialize this action.
        /// </summary>
        /// <param name="context">Context of the project.</param>
        public override void Init(IProjectContext context)
        {
            base.Init(context);
        }
    }
}