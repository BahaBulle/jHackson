// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Scripts.Tables
{
    using JHackson.Core.Common;

    public class Plugin : IPlugin
    {
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionTableLoad));
            PluginsHelper.LoadAction(typeof(ActionTableSave));
        }
    }
}