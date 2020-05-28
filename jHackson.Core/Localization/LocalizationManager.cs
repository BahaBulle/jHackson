// <copyright file="LocalizationManager.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    public static class LocalizationManager
    {
        static LocalizationManager()
        {
            SupportedCultures = new Dictionary<CultureInfo, ILocalizationResourceProvider>();
        }

        public static EventHandler CultureChanged { get; set; }

        public static CultureInfo CurrentCulture
        {
            get => CultureInfo.CurrentUICulture;

            set
            {
                if (!Thread.CurrentThread.CurrentUICulture.Equals(value) || LocalisationProvider == null)
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("value", "Current culture cannot be null.");
                    }

                    if (!IsSupported(value))
                    {
                        throw new ArgumentException(
                            $"The culture {value.DisplayName} is not supported.",
                            "value");
                    }

                    if (LocalisationProvider != null)
                    {
                        LocalisationProvider.Unload();
                    }

                    if (SupportedCultures.TryGetValue(value, out var newProvider) && newProvider != null)
                    {
                        Thread.CurrentThread.CurrentUICulture = value;

                        LocalisationProvider = newProvider;
                        LocalisationProvider.Load();

                        CultureChanged?.Invoke(null, EventArgs.Empty);
                    }
                    else
                    {
                        throw new ArgumentException($"No provider has been provided for culture {value.DisplayName}.", "value");
                    }
                }
            }
        }

        public static ILocalizationResourceProvider LocalisationProvider { get; private set; }

        public static IDictionary<CultureInfo, ILocalizationResourceProvider> SupportedCultures { get; private set; }

        public static string GetMessage(string key, params object[] parameters)
        {
            if (LocalisationProvider != null)
            {
                var message = LocalisationProvider.GetValue(key);

                return message == null ? $"?[{key}]?" : string.Format((string)message, parameters);
            }

            return $"?[{key}]?";
        }

        public static string GetMessage(string key)
        {
            if (LocalisationProvider != null)
            {
                string message = (string)LocalisationProvider.GetValue(key);

                if (message == null)
                {
                    message = $"?[{key}]?";
                }

                return message;
            }

            return $"?[{key}]?";
        }

        private static bool IsSupported(CultureInfo culture)
        {
            return SupportedCultures.ContainsKey(culture);
        }
    }
}