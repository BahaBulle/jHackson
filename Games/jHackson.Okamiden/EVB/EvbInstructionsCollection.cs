// <copyright file="EvbInstructionsCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;

    internal class EvbInstructionsCollection : Collection<long>
    {
        private readonly EvbHeader header;
        private readonly BinaryReader reader;

        internal EvbInstructionsCollection(BinaryReader binaryReader, EvbHeader header)
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
                long value = EvbHelper.ReadInteger(this.reader, this.header.IsLittleEndian, this.header.SizeOfInstruction);

                this.Add(value);
            }
        }
    }
}
