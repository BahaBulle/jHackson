// <copyright file="EvbConstantsCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;

    internal class EvbConstantsCollection : Collection<object>
    {
        private readonly EvbHeader header;
        private readonly BinaryReader reader;

        public EvbConstantsCollection(BinaryReader binaryReader, EvbHeader header)
        {
            this.reader = binaryReader;
            this.header = header;

            this.Parse();
        }

        private void Parse()
        {
            long numberOfElement = EvbHelper.ReadInteger(this.reader, this.header.IsLittleEndian, this.header.SizeOfInt);

            for (int i = 0; i < numberOfElement; i++)
            {
                byte type = this.reader.ReadByte();

                EvbConstant? constant = null;
                switch (type)
                {
                    case 1:
                        constant = new EvbConstant(type, this.reader.ReadBoolean());
                        break;

                    case 3:
                        constant = new EvbConstant(type, EvbHelper.ReadNumber(this.reader, this.header.SizeOfLuaNumber));
                        break;

                    case 4:
                        constant = new EvbConstant(type, EvbHelper.ReadString(this.reader, this.header.IsLittleEndian, this.header.SizeOfInt));
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
