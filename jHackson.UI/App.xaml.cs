using jHackson.Core.Services;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Linq;
using System.Windows;

namespace jHackson
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var args = Environment.GetCommandLineArgs()
                .ToList();

            if (args.Any())
                return null;
            else
                return this.Container.Resolve<MainView>();
        }

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var args = Environment.GetCommandLineArgs()
                .ToList();

            if (args.Any())
            {
                var batch = this.Container.Resolve<IBatch>();

                batch.Start(args);

                if (Current != null)
                    Current.Shutdown();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Enregistrement des services nécessaires
            containerRegistry.Register<ISerializationService, SerializationService>();

            // Enregistrement des objects
            //containerRegistry.Register<IBatch, MainBatch>();
        }
    }
}