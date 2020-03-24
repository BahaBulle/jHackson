using jHackson.Core.Projects;
using System.Collections.Generic;

namespace jHackson.Core.Actions
{
    public interface IActionJson
    {
        #region Properties

        bool HasErrors { get; }
        string Name { get; }
        string Title { get; set; }
        bool? Todo { get; set; }

        #endregion

        #region Errors methods

        void AddError(string message);

        List<string> GetErrors();

        #endregion

        #region Methods

        void Check();

        void Execute();

        void Init(IProjectContext context);

        #endregion
    }
}