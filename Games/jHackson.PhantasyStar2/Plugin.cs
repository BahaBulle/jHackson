// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.PhantasyStar2
{
    using JHackson.Core.Common;
    using JHackson.PhantasyStar2.Actions;

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
            PluginsHelper.LoadAction(typeof(ActionGraphismCompression));
            PluginsHelper.LoadAction(typeof(ActionGraphismDecompression));
        }
    }
}