// <copyright file="EvbConstant.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    internal class EvbConstant
    {
        public EvbConstant(byte type, bool value)
        {
            this.Type = type;
            this.Bool = value;
        }
        public EvbConstant(byte type, double value)
        {
            this.Type = type;
            this.Double = value;
        }
        public EvbConstant(byte type, string? value)
        {
            this.Type = type;
            this.String = value;
        }

        public bool? Bool { get; private set; }

        public double? Double { get; private set; }

        public bool IsNil => !this.Bool.HasValue && !this.Double.HasValue && this.String != null;

        public string? String { get; private set; }

        public byte Type { get; private set; }
    }
}
