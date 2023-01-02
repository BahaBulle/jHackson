// <copyright file="EvbUpValuesCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;
    using System.IO;

    internal class EvbUpValuesCollection : Collection<string?>
    {
        internal EvbUpValuesCollection(BinaryReader binaryReader, EvbHeader header)
        {
            ulong numberOfElement = EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInt);

            for (ulong i = 0; i < numberOfElement; i++)
            {
                string? value = EvbHelper.ReadString(binaryReader, header.IsLittleEndian, header.SizeOfInstruction);

                this.Add(value);
            }
        }
    }
}
