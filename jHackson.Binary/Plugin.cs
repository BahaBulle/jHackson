// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Binary
{
    using JHackson.Binary.Actions;
    using JHackson.Binary.FileFormat;
    using JHackson.Core.Common;

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