// <copyright file="EvbFunctionsCollection.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.ObjectModel;

    internal class EvbFunctionsCollection : Collection<EvbFunction>
    {
        private readonly EvbHeader header;
        private readonly BinaryReader reader;

        public EvbFunctionsCollection(BinaryReader binaryReader, EvbHeader header)
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
                var function = new EvbFunction(this.reader, this.header, $"Function_{i}");

                this.Add(function);
            }
        }
    }
}
