using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Localization;
using jHackson.Core.Projects;
using NLog;

namespace jHackson.Actions
{
    public class ActionBinSave : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionBinSave()
        {
            this.Name = "BinSave";
            this.Title = null;
            this.Todo = true;

            this.FileName = null;
            this.Format = null;
            this.From = null;
        }

        public string FileName { get; set; }
        public string Format { get; set; }
        public int? From { get; set; }

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.FileName), this.FileName ?? "null"));

            if (!this.From.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString() : "null"));

            if (!this._context.BufferExists(this.From.Value))
                this.AddError(LocalizationManager.GetMessage("core.bufferUnknow", this.From));

            if (string.IsNullOrWhiteSpace(this.Format))
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.Format), this.Format ?? "null"));

            if (!DataContext.FormatExists(this.Format))
                this.AddError(LocalizationManager.GetMessage("core.formatUnknow", this.From));
        }

        public override void Execute()
        {
            if (this.Todo.HasValue && this.Todo.Value)
            {
                if (this.Title != null)
                    _logger.Info(this.Title);

                var obj = this._context.GetBuffer(this.From.Value);
                var format = DataContext.GetFormat(this.Format);

                format.Save(this.FileName, obj);
            }
        }

        public override void Init(IProjectContext context)
        {
            this.FileName = Helper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);
        }
    }
}