// <copyright file="Plugin.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Image
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using JHackson.Core.Common;
    using JHackson.Image.Actions;
    using JHackson.Image.FileFormat;
    using JHackson.Image.ImageFormat;

    public class Plugin : IPlugin
    {
        public void Init()
        {
            PluginsHelper.LoadAction(typeof(ActionBmpLoad));
            PluginsHelper.LoadAction(typeof(ActionImgSave));
            PluginsHelper.LoadAction(typeof(ActionPngLoad));

            LoadImageFormat(typeof(Linear1BPP));
            LoadImageFormat(typeof(Linear4BPP));
            LoadImageFormat(typeof(Planar2BPP));

            PluginsHelper.LoadFileFormat(typeof(FileFormatBmp));
            PluginsHelper.LoadFileFormat(typeof(FileFormatPng));
        }

        private static void LoadImageFormat(Type elementType)
        {
            var format = (IImageFormat)elementType.InvokeMember(
                null,
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null,
                CultureInfo.InvariantCulture);

            if (format != null)
            {
                DataContext.AddImageFormat(format.Name, elementType);
            }
        }
    }
}