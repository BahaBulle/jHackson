using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Exceptions;
using jHackson.Core.Localization;
using jHackson.Core.Projects;
using NLog;
using System.IO;

namespace jHackson.Actions.Actions
{
    public class ActionBinLoadCopy : ActionBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionBinLoadCopy()
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
        public string FileName { get; set; }
        public BufferParameters Source { get; set; }
        public int? To { get; set; }

        public override void Check()
        {
            if (string.IsNullOrWhiteSpace(this.FileName) || !File.Exists(this.FileName))
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.FileName), this.FileName ?? "null"));

            if (!this.To.HasValue)
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString() : "null"));
        }

        public override void Execute()
        {
            if (this.Todo.HasValue && this.Todo.Value)
            {
                if (this.Title != null)
                    _logger.Info(this.Title);

                var msSource = new MemoryStream(File.ReadAllBytes(this.FileName));
                var msDest = this._context.GetBufferMemoryStream(this.To.Value, true);

                if (!this.Source.AdressStart.HasValue)
                    this.Source.AdressStart = 0;

                if (!this.Destination.AdressStart.HasValue)
                    this.Destination.AdressStart = 0;

                if (!this.Source.Size.HasValue && !this.Source.AdressEnd.HasValue)
                    this.Source.Size = msSource.Length - this.Source.AdressStart;

                if (!this.Destination.Size.HasValue && !this.Destination.AdressEnd.HasValue)
                    this.Destination.Size = this.Source.Size;

                if (this.Source.Size.Value > this.Destination.Size.Value)
                    throw new JHacksonException(LocalizationManager.GetMessage("actions.notEnoughSpace", this.Source.Size, this.Destination.Size));

                var bytes = new byte[this.Source.Size.Value];
                msSource.Position = this.Source.AdressStart.Value;
                msSource.Read(bytes, 0, (int)this.Source.Size.Value);

                msDest.Position = this.Destination.AdressStart.Value;
                msDest.Write(bytes, 0, (int)this.Source.Size.Value);
            }
        }

        public override void Init(IProjectContext context)
        {
            this.FileName = Helper.ReplaceVariables(context.GetVariables(), this.FileName);

            base.Init(context);

            this.Source.Init();
            this.Destination.Init();
        }
    }
}