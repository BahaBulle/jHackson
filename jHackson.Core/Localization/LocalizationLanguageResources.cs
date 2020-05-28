// <copyright file="LocalizationLanguageResources.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Localization
{
    using System.Globalization;
    using Newtonsoft.Json.Linq;

    public class LocalizationLanguageResources
    {
        public CultureInfo Culture { get; set; }

        public JToken Value { get; set; }
    }
}