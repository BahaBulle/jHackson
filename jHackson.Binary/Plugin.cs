// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary
{
    using JHackson.Binary.Actions;
    using JHackson.Binary.FileFormat;
    using JHackson.Core.Common;

    public class Plugin : IPlugin
    {
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionBinCopy));
            PluginsHelper.LoadAction(typeof(ActionBinInsert));
            PluginsHelper.LoadAction(typeof(ActionBinLoad));
            PluginsHelper.LoadAction(typeof(ActionBinLoadCopy));
            PluginsHelper.LoadAction(typeof(ActionBinModify));
            PluginsHelper.LoadAction(typeof(ActionBinSave));

            PluginsHelper.LoadFileFormat(typeof(FileFormatBin));
        }
    }
}