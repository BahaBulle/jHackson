// <copyright file="ContextInfo.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm.Common
{
    using System;

    public struct ContextInfo : IEquatable<ContextInfo>
    {
        public byte MPS { get; set; }

        public byte Status { get; set; }

        public static bool operator !=(ContextInfo left, ContextInfo right) => !(left == right);

        public static bool operator ==(ContextInfo left, ContextInfo right) => left.Equals(right);

        public override bool Equals(object obj)
        {
            return (obj is ContextInfo contextInfo) && this.Equals(contextInfo);
        }

        public bool Equals(ContextInfo other)
        {
            return this.MPS == other.MPS && this.Status == other.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.MPS, this.Status);
        }
    }
}