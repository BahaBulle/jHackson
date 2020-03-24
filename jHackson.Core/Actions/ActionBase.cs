using jHackson.Core.Common;
using jHackson.Core.Projects;
using System.Collections.Generic;

namespace jHackson.Core.Actions
{
    public abstract class ActionBase : IActionJson
    {
        #region Fields

        protected IProjectContext _context;
        private readonly List<string> _Errors;

        #endregion

        #region Properties

        public bool HasErrors => this._Errors.Count > 0;
        public string Name { get; set; }

        public string Title { get; set; }

        public bool? Todo { get; set; }

        #endregion

        #region Constructors

        protected ActionBase()
        {
            this._Errors = new List<string>();
        }

        #endregion

        #region Error methods

        public virtual void AddError(string message)
        {
            this._Errors.Add($"{this.Name} - {message}");
        }

        public virtual List<string> GetErrors()
        {
            return this._Errors;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check if mandatories properties have values
        /// </summary>
        public abstract void Check();

        public abstract void Execute();

        public virtual void Init(IProjectContext context)
        {
            this._context = context;

            this.Title = Helper.ReplaceParameters(this, this.Title);
        }

        #endregion
    }
}