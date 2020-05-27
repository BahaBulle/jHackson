using jHackson.Core.Actions;
using jHackson.Core.Localization;
using jHackson.StarOcean.SDD1Algorithm;
using NLog;

namespace jHackson.StarOcean.Actions
{
    public class ActionSDD1Compression : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString() : "null"));

            if (!this.To.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString() : "null"));
        }

        public override void Execute()
        {
            if (this.Title != null)
                _logger.Info(this.Title);

            var msSource = this._context.GetBufferMemoryStream(this.From.Value);

            ISDD1Comp sdd1 = new SDD1();

            var msDestination = sdd1.Compress(msSource);

            this._context.AddBuffer(this.To.Value, msDestination);
        }
    }
}