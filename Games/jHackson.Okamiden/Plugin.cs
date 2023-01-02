// <copyright file="Plugin.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("jHackson.Okamiden.Tests")]

namespace jHackson.Okamiden
{
    using JHackson.Core.Common;

    public class Plugin : IPlugin
    {
        /// <summary>
        /// Initialize actions of this library.
        /// </summary>
        public void Init()
        {
        }
    }
}
