// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers
{
    using JHackson.Core.Common;
    using JHackson.Text.Pointers.Actions;

    /// <summary>
    /// Provides a class that to allow to load this library dynamically.
    /// </summary>
    public class Plugin : IPlugin
    {
        /// <summary>
        /// Initialize actions of this library.
        /// </summary>
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionPointerLoad));
        }
    }
}