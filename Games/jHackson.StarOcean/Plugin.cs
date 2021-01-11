// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean
{
    using JHackson.Core.Common;
    using JHackson.StarOcean.Actions;

    public class Plugin : IPlugin
    {
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionSDD1Compression));
            PluginsHelper.LoadAction(typeof(ActionSDD1Decompression));
        }
    }
}