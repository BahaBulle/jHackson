// <copyright file="EvbLocal.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    internal class EvbLocal
    {
        public EvbLocal(string? name, ulong start, ulong end)
        {
            this.Name = name;
            this.Start = start;
            this.End = end;
        }

        public ulong End { get; private set; }

        public string? Name { get; private set; }

        public ulong Start { get; private set; }
    }
}
