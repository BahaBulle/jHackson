using jHackson.Core.Actions;
using jHackson.Core.Localization;
using jHackson.StarOcean.SDD1Algorithm;
using NLog;

namespace jHackson.StarOcean.Actions
{
    public class ActionSDD1Decompression : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionSDD1Decompression()
        {
            this.Name = "SOSDD1Decompression";
            this.Title = null;
            this.Todo = true;

            this.From = null;
            this.To = null;
            this.SizeOut = null;
        }

        public int? From { get; set; }
        public ushort? SizeOut { get; set; }
        public int? To { get; set; }

        public override void Check()
        {
            if (!this.From.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString() : "null"));

            if (!this.To.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString() : "null"));

            if (!this.SizeOut.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.SizeOut), this.SizeOut.HasValue ? this.SizeOut.Value.ToString() : "null"));

            if (this.SizeOut.Value > 0xFFFF)
                this.AddError(LocalizationManager.GetMessage("starocean.sdd1.incorrectSizeOut"));
        }

        public override void Execute()
        {
            if (this.Title != null)
                _logger.Info(this.Title);

            var msSource = this._context.GetBufferMemoryStream(this.From.Value);

            ISDD1Decomp sdd1 = new SDD1();

            var msDestination = sdd1.Decompress(msSource, this.SizeOut.Value);

            this._context.AddBuffer(this.To.Value, msDestination);
        }
    }
}