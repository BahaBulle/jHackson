// <copyright file="EvbFunction.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.IO;

    internal class EvbFunction
    {
        public EvbFunction(BinaryReader binaryReader, EvbHeader header, string id)
        {
            this.Id = id;

            this.Name = EvbHelper.ReadString(binaryReader, header.IsLittleEndian, header.SizeOfSizeT);
            this.LineStart = EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInt);
            this.LineEnd = EvbHelper.ReadInteger(binaryReader, header.IsLittleEndian, header.SizeOfInt);
            this.NumberOfUpValues = binaryReader.ReadByte();
            this.NumberOfParams = binaryReader.ReadByte();
            this.VarArg = binaryReader.ReadByte();
            this.MaxStack = binaryReader.ReadByte();

            this.Instructions = new EvbInstructionsCollection(binaryReader, header);
            this.Constants = new EvbConstantsCollection(binaryReader, header);
            this.Functions = new EvbFunctionsCollection(binaryReader, header);
            this.LinesPositions = new EvbLinesPositionsCollection(binaryReader, header);
            this.Locals = new EvbLocalsCollection(binaryReader, header);
            this.UpValues = new EvbUpValuesCollection(binaryReader, header);
        }

        internal EvbConstantsCollection Constants { get; private set; }

        internal EvbFunctionsCollection Functions { get; private set; }

        internal string Id { get; private set; }

        internal EvbInstructionsCollection Instructions { get; private set; }

        internal ulong LineEnd { get; private set; }

        internal EvbLinesPositionsCollection LinesPositions { get; private set; }

        internal ulong LineStart { get; private set; }

        internal EvbLocalsCollection Locals { get; private set; }

        internal byte MaxStack { get; private set; }

        internal string? Name { get; private set; }

        internal byte NumberOfParams { get; private set; }

        internal byte NumberOfUpValues { get; private set; }

        internal EvbUpValuesCollection UpValues { get; private set; }

        internal byte VarArg { get; private set; }
    }
}
