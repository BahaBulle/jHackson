// <copyright file="EvbFunction.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    internal class EvbFunction
    {
        private readonly EvbHeader header;
        private readonly BinaryReader reader;

        public EvbFunction(BinaryReader binaryReader, EvbHeader header, string id)
        {
            this.reader = binaryReader;
            this.header = header;
            this.Id = id;

            this.Parse();
        }

        private void Parse()
        {
            this.Name = EvbHelper.ReadString(this.reader, this.header.IsLittleEndian, this.header.SizeOfSizeT);
            this.LineStart = EvbHelper.ReadInteger(this.reader, this.header.IsLittleEndian, this.header.SizeOfInt);
            this.LineEnd = EvbHelper.ReadInteger(this.reader, this.header.IsLittleEndian, this.header.SizeOfInt);
            this.NumberOfUpValues = this.reader.ReadByte();
            this.NumberOfParams = this.reader.ReadByte();
            this.VarArg = this.reader.ReadByte();
            this.MaxStack = this.reader.ReadByte();

            this.Instructions = new EvbInstructionsCollection(this.reader, this.header);
            this.Constants = new EvbConstantsCollection(this.reader, this.header);
        }

        internal EvbConstantsCollection? Constants { get; private set; }

        internal string Id { get; private set; }

        internal EvbInstructionsCollection? Instructions { get; private set; }

        internal List<EvbInstructionsCollection> InstructionsList { get; private set; } = new List<EvbInstructionsCollection>();

        internal long LineEnd { get; private set; }

        internal long LineStart { get; private set; }

        internal byte MaxStack { get; private set; }

        internal string? Name { get; private set; }

        internal byte NumberOfParams { get; private set; }

        internal byte NumberOfUpValues { get; private set; }

        internal byte VarArg { get; private set; }
    }
}
