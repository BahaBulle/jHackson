using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Projects;
using NLog;
using System.IO;

namespace jHackson.Actions
{
    public class ActionFileLoad : ActionBase
    {
        #region Fields

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        public string FileName { get; set; }
        public int? To { get; set; }

        #endregion

        #region Constructors

        public ActionFileLoad()
        {
            this.Name = "FileLoad";
            this.Title = string.Empty;
            this.Todo = true;

            this.FileName = null;
            this.To = null;
        }

        #endregion

        #region Methods

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName) || !File.Exists(this.FileName))
                this.AddError($"Parameter 'FileName' not found : {this.FileName ?? "null"}");

            if (!this.To.HasValue)
                this.AddError($"Parameter 'To' not found : {(this.To.HasValue ? this.To.Value.ToString() : "null")}");
        }

        public override void Execute()
        {
            if (this.Todo.HasValue && this.Todo.Value)
            {
                if (this.Title != null)
                    _logger.Info(this.Title);

                var ms = new MemoryStream(File.ReadAllBytes(this.FileName));
                this._context.AddBuffer(this.To.Value, ms);
            }
        }

        public override void Init(IProjectContext context)
        {
            this.FileName = Helper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);
        }

        #endregion
    }
}