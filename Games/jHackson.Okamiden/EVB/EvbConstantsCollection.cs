// <copyright file="EvbConstantsCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;
    using System.IO;

    internal class EvbConstantsCollection : Collection<EvbConstant>
    {
        public EvbConstantsCollection(BinaryReader binaryReader, EvbHeader header)
        {
            ulong numberOfElement = EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInt);

            for (ulong i = 0; i < numberOfElement; i++)
            {
                byte type = binaryReader.ReadByte();

                EvbConstant? constant = null;
                switch (type)
                {
                    case 1:
                        constant = new EvbConstant(type, binaryReader.ReadBoolean());
                        break;

                    case 3:
                        constant = new EvbConstant(type, EvbHelper.ReadNumber(binaryReader, header.SizeOfLuaNumber));
                        break;

                    case 4:
                        constant = new EvbConstant(type, EvbHelper.ReadString(binaryReader, header.IsLittleEndian, header.SizeOfInt));
                        break;

                }

                if (constant != null)
                {
                    this.Add(constant);
                }
            }
        }
    }
}
