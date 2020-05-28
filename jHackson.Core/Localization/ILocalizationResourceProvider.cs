// <copyright file="ILocalizationResourceProvider.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Localization
{
    public interface ILocalizationResourceProvider
    {
        object GetValue(string key);

        void Load();

        void Unload();
    }
}