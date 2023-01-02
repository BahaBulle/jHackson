// <copyright file="EvbLocalsCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;
    using System.IO;

    internal class EvbLocalsCollection : Collection<EvbLocal>
    {
        internal EvbLocalsCollection(BinaryReader binaryReader, EvbHeader header)
        {
            ulong numberOfElement = EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInt);

            for (ulong i = 0; i < numberOfElement; i++)
            {
                var local = new EvbLocal(
                    EvbHelper.ReadString(binaryReader, header.IsLittleEndian, header.SizeOfInt),
                    EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInstruction),
                    EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInstruction));

                this.Add(local);
            }
        }
    }
}
