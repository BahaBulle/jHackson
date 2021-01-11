// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.PhantasyStar2
{
    using JHackson.Core.Common;
    using JHackson.PhantasyStar2.Actions;

    public class Plugin : IPlugin
    {
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionGraphismCompression));
            PluginsHelper.LoadAction(typeof(ActionGraphismDecompression));
        }
    }
}