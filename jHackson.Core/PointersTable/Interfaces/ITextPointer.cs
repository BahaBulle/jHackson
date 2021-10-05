// <copyright file="ITextPointer.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.PointersTable
{
    public interface ITextPointer
    {
        long Adress { get; set; }

        string Calculation { get; set; }

        EnumEndianType Endian { get; set; }

        int Id { get; set; }

        short NumberOfBytes { get; set; }

        ulong Value { get; set; }
    }
}