using jHackson.Core.Common;
using jHackson.Core.Projects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace jHackson.Core.Actions
{
    public abstract class ActionBase : IActionJson
    {
        protected IProjectContext _context;
        private readonly List<string> _Errors;

        protected ActionBase()
        {
            this._Errors = new List<string>();
        }

        [JsonIgnore]
        public bool HasErrors => this._Errors.Count > 0;

        [JsonIgnore]
        public string Name { get; protected set; }

        public string Title { get; set; }

        public bool? Todo { get; set; }

        public virtual void AddError(string message)
        {
            this._Errors.Add($"{this.Name} - {message}");
        }

        public virtual void AddErrors(List<string> errors)
        {
            this._Errors.AddRange(errors);
        }

        /// <summary>
        /// Check if mandatories properties have values
        /// </summary>
        public abstract void Check();

        public abstract void Execute();

        public virtual List<string> GetErrors()
        {
            return this._Errors;
        }

        public virtual void Init(IProjectContext context)
        {
            this._context = context;

            this.Title = Helper.ReplaceParameters(this, this.Title);
        }
    }
}