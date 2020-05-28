// <copyright file="TableElementParam.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.TableElements
{
    public class TableElementParam : ITableElementParam
    {
        public int NbBytes { get; set; }

        public int Position { get; set; }

        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            var param = (TableElementParam)obj;

            return this.NbBytes == param.NbBytes && this.Position == param.Position && this.Value == param.Value;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(this.NbBytes, this.Position, this.Value);
        }
    }
}