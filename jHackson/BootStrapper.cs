using jHackson.Core.Common;
using jHackson.Core.Json.ContractResolver;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Projects;
using jHackson.Core.Services;
using Unity;

namespace jHackson
{
    public class BootStrapper
    {
        private static IUnityContainer _container;

        public static void Init(IUnityContainer container)
        {
            _container = container;

            // Register IoC
            RegisterElements();

            // Load plugins
            Helper.LoadPlugins();
        }

        private static void RegisterElements()
        {
            _container.RegisterType<IProjectJson, ProjectJson>();
            _container.RegisterType<ISerializationService, SerializationService>();

            _container.RegisterType<ActionJsonConverter>();
            _container.RegisterType<VariableJsonConverter>();
            _container.RegisterType<Batch>();
            _container.RegisterType<UnityContractResolver>();
        }
    }
}