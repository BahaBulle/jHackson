using jHackson.Core.Projects;

namespace jHackson.Core.Services
{
    public interface ISerializationService
    {
        IProjectJson Deserialize(string filename);

        void Serialize(IProjectJson project, string filename);
    }
}