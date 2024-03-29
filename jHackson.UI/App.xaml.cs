﻿namespace JHackson.UI
{
    using System.Windows;
    using JHackson.Core.Common;
    using JHackson.Core.Json.JsonConverters;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using JHackson.Core.Services;
    using JHackson.UI.Views;
    using Prism.Ioc;
    using Prism.Unity;

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
            LocalizationHelper.InitCulture();

            PluginsHelper.LoadPlugins();

            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IProjectJson, ProjectJson>();
            containerRegistry.Register<ISerializationService, SerializationService>();

            containerRegistry.Register<ActionJsonConverter>();
        }
    }
}