// <copyright file="LocalizationHelper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Localization
{
    using System.Globalization;
    using JHackson.Core.Localization.Providers;

    public static class LocalizationHelper
    {
        public static void InitCulture()
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