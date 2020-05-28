// <copyright file="Program.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson
{
    using System.Linq;
    using Unity;

    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            BootStrapper.Init(container);

            container.Resolve<Batch>().Run(args.ToList());
        }
    }
}