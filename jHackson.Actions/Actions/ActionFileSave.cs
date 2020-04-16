using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Projects;
using NLog;

namespace jHackson.Actions
{
    public class ActionFileSave : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionFileSave()
        {
            this.Name = "FileSave";
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
                this.AddError($"Parameter 'FileName' not found : {this.FileName ?? "null"}");

            if (!this.From.HasValue)
                this.AddError($"Parameter 'From' not found : {(this.From.HasValue ? this.From.Value.ToString() : "null")}");

            if (string.IsNullOrWhiteSpace(this.Format))
                this.AddError($"Parameter 'Format' not found : {this.Format ?? "null"}");
        }

        public override void Execute()
        {
            if (this.Todo.HasValue && this.Todo.Value)
            {
                if (this.Title != null)
                    _logger.Info(this.Title);

                var obj = this._context.GetBuffer(this.From.Value);
                var format = DataContext.GetFormat(this.Format);

                if (obj == null)
                    this.AddError($"Buffer {this.From.Value} doesn't not exist!");

                if (format == null)
                    this.AddError($"Format {this.Format} doesn't not exist!");

                if (!this.HasErrors)
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