using jHackson.Core.Projects;
using System.Collections.Generic;

namespace jHackson.Core.Actions
{
    public interface IActionJson
    {
        bool HasErrors { get; }
        string Name { get; }
        string Title { get; set; }
        bool? Todo { get; set; }

        void AddError(string message);

        void Check();

        void Execute();

        List<string> GetErrors();

        void Init(IProjectContext context);
    }
}