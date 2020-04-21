using jHackson.Core.Common;
using jHackson.Core.Json.ContractResolver;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Localization;
using jHackson.Core.Localization.Providers;
using jHackson.Core.Projects;
using jHackson.Core.Services;
using jHackson.UI.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Globalization;
using System.Windows;

namespace jHackson.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainView>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.InitCulture();

            Helper.LoadPlugins();

            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IProjectJson, ProjectJson>();
            containerRegistry.Register<ISerializationService, SerializationService>();

            containerRegistry.Register<ActionJsonConverter>();
            containerRegistry.Register<VariableJsonConverter>();
            containerRegistry.Register<UnityContractResolver>();
        }

        private void InitCulture()
        {
            var provider = new JsonLocalizationProvider();
            provider.Init();

            foreach (var culture in provider.CulturesList)
            {
                LocalizationManager.SupportedCultures.Add(culture, provider);
            }

            LocalizationManager.CurrentCulture = CultureInfo.CurrentUICulture;
        }
    }
}