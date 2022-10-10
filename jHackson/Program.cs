// <copyright file="Program.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson
{
    using System.Linq;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.Services;

    internal class Program
    {
        private static void Main(string[] args)
        {
            LocalizationHelper.InitCulture();

            // Load plugins
            PluginsHelper.LoadPlugins();

            var service = new SerializationService();

            var batch = new Batch(service);

            batch.Run(args.ToList());
        }
    }
}