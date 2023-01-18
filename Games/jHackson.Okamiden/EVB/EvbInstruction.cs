// <copyright file="EvbInstruction.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.EVB
{
    using System.Collections.Generic;

    internal class EvbInstruction
    {
        private readonly Dictionary<int, string> opCodes = new Dictionary<int, string>
        {
            [0x00] = "MOVE",
            [0x01] = "LOADK",
            [0x02] = "LOADBOOL",
            [0x03] = "LOADNIL",
            [0x04] = "GETUPVAL",
            [0x05] = "GETGLOBAL",
            [0x06] = "GETTABLE",
            [0x07] = "SETGLOBAL",
            [0x08] = "SETUPVAL",
            [0x09] = "SETTABLE",
            [0x0A] = "NEWTABLE",
            [0x0B] = "SELF",
            [0x0C] = "ADD",
            [0x0D] = "SUB",
            [0x0E] = "MUL",
            [0x0F] = "DIV",
            [0x10] = "MOD",
            [0x11] = "POW",
            [0x12] = "UNM",
            [0x13] = "NOT",
            [0x14] = "LEN",
            [0x15] = "CONCAT",
            [0x16] = "JMP",
            [0x17] = "EQ",
            [0x18] = "LT",
            [0x19] = "LE",
            [0x1A] = "TEST",
            [0x1B] = "TESTSET",
            [0x1C] = "CALL",
            [0x1D] = "TAILCALL",
            [0x1E] = "RETURN",
            [0x1F] = "FORLOOP",
            [0x20] = "FORPREP",
            [0x21] = "TFORLOOP",
            [0x22] = "SETLIST",
            [0x23] = "CLOSE",
            [0x24] = "CLOSURE",
            [0x25] = "VARARG",
        };

        public EvbInstruction(ulong value)
        {
            this.Value = value;

            this.Code = value & 0x3F;
            this.A = (value >> 6) & 0xFF;
            this.B = (value >> 6 + 8 + 9) & 0x1FF;
            this.C = (value >> 6 + 8) & 0x1FF;
            this.BC = value >> (6 + 8);
        }

        public ulong A { get; private set; }

        public ulong B { get; private set; }

        public ulong BC { get; private set; }

        public ulong C { get; private set; }

        public ulong Code { get; private set; }

        public string OpCode => this.opCodes[(int)this.Code];

        public ulong Value { get; private set; }
    }
}
