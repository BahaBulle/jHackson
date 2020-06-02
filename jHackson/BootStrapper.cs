// <copyright file="BootStrapper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson
{
    using System.Globalization;
    using JHackson.Core.Common;
    using JHackson.Core.Json.ContractResolver;
    using JHackson.Core.Json.JsonConverters;
    using JHackson.Core.Localization;
    using JHackson.Core.Localization.Providers;
    using JHackson.Core.Projects;
    using JHackson.Core.Services;
    using Unity;

    public static class BootStrapper
    {
        private static IUnityContainer container;

        public static void Init(IUnityContainer container)
        {
            BootStrapper.container = container;

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
            container.RegisterType<IProjectJson, ProjectJson>();
            container.RegisterType<ISerializationService, SerializationService>();

            container.RegisterType<ActionJsonConverter>();
            container.RegisterType<VariableJsonConverter>();
            container.RegisterType<Batch>();
            container.RegisterType<UnityContractResolver>();
        }
    }
}