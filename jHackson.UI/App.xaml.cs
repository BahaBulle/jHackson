using jHackson.Core.Localization;
using jHackson.Core.Localization.Providers;
using System.Globalization;
using System.Windows;

namespace jHackson
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //protected override Window CreateShell()
        //{
        //    var args = Environment.GetCommandLineArgs()
        //        .ToList();

        //    if (args.Any())
        //        return null;
        //    else
        //        return this.Container.Resolve<MainView>();
        //}

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    var args = Environment.GetCommandLineArgs()
        //        .ToList();

        //    if (args.Any())
        //    {
        //        var batch = this.Container.Resolve<IBatch>();

        //        batch.Start(args);

        //        if (Current != null)
        //            Current.Shutdown();
        //    }
        //}

        //protected override void RegisterTypes(IContainerRegistry containerRegistry)
        //{
        //    // Enregistrement des services nécessaires
        //    containerRegistry.Register<ISerializationService, SerializationService>();

        //    // Enregistrement des objects
        //    //containerRegistry.Register<IBatch, MainBatch>();
        //}

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