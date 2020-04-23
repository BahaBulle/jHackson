using jHackson.Core.Common;
using jHackson.Core.Json.ContractResolver;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Localization;
using jHackson.Core.Localization.Providers;
using jHackson.Core.Projects;
using jHackson.Core.Services;
using System.Globalization;
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

            InitCulture();

            // Load plugins
            Helper.LoadPlugins();
        }

        private static void InitCulture()
        {
            var provider = new JsonLocalizationProvider();
            provider.Init();

            foreach (var culture in provider.CulturesList)
            {
                LocalizationManager.SupportedCultures.Add(culture, provider);
            }

            LocalizationManager.CurrentCulture = CultureInfo.CurrentUICulture;
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