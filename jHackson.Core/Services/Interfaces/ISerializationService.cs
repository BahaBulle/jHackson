using jHackson.Core.Projects;

namespace jHackson.Core.Services
{
    public interface ISerializationService
    {
        #region Methods

        IProjectJson Deserialize(string filename);

        void Serialize(IProjectJson project, string filename);

        #endregion
    }
}