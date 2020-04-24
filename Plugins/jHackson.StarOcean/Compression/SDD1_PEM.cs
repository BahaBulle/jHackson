using System;

namespace jHackson.StarOcean.Compression
{
    //Probability Estimation Module
    internal class SDD1_PEM
    {
        private static readonly State[] evolution_table = new State[] {
            new State{ code_num = 0, nextIfLPS = 25, nextIfMPS = 25},
            new State{ code_num = 0, nextIfLPS =  2, nextIfMPS =  1},
            new State{ code_num = 0, nextIfLPS =  3, nextIfMPS =  1},
            new State{ code_num = 0, nextIfLPS =  4, nextIfMPS =  2},
            new State{ code_num = 0, nextIfLPS =  5, nextIfMPS =  3},
            new State{ code_num = 1, nextIfLPS =  6, nextIfMPS =  4},
            new State{ code_num = 1, nextIfLPS =  7, nextIfMPS =  5},
            new State{ code_num = 1, nextIfLPS =  8, nextIfMPS =  6},
            new State{ code_num = 1, nextIfLPS =  9, nextIfMPS =  7},
            new State{ code_num = 2, nextIfLPS = 10, nextIfMPS =  8},
            new State{ code_num = 2, nextIfLPS = 11, nextIfMPS =  9},
            new State{ code_num = 2, nextIfLPS = 12, nextIfMPS = 10},
            new State{ code_num = 2, nextIfLPS = 13, nextIfMPS = 11},
            new State{ code_num = 3, nextIfLPS = 14, nextIfMPS = 12},
            new State{ code_num = 3, nextIfLPS = 15, nextIfMPS = 13},
            new State{ code_num = 3, nextIfLPS = 16, nextIfMPS = 14},
            new State{ code_num = 3, nextIfLPS = 17, nextIfMPS = 15},
            new State{ code_num = 4, nextIfLPS = 18, nextIfMPS = 16},
            new State{ code_num = 4, nextIfLPS = 19, nextIfMPS = 17},
            new State{ code_num = 5, nextIfLPS = 20, nextIfMPS = 18},
            new State{ code_num = 5, nextIfLPS = 21, nextIfMPS = 19},
            new State{ code_num = 6, nextIfLPS = 22, nextIfMPS = 20},
            new State{ code_num = 6, nextIfLPS = 23, nextIfMPS = 21},
            new State{ code_num = 7, nextIfLPS = 24, nextIfMPS = 22},
            new State{ code_num = 7, nextIfLPS = 24, nextIfMPS = 23},
            new State{ code_num = 0, nextIfLPS = 26, nextIfMPS =  1},
            new State{ code_num = 1, nextIfLPS = 27, nextIfMPS =  2},
            new State{ code_num = 2, nextIfLPS = 28, nextIfMPS =  4},
            new State{ code_num = 3, nextIfLPS = 29, nextIfMPS =  8},
            new State{ code_num = 4, nextIfLPS = 30, nextIfMPS = 12},
            new State{ code_num = 5, nextIfLPS = 31, nextIfMPS = 16},
            new State{ code_num = 6, nextIfLPS = 32, nextIfMPS = 18},
            new State{ code_num = 7, nextIfLPS = 24, nextIfMPS = 22}
        };

        private readonly SDD1_BG[] _BG;

        private readonly SDD1_ContextInfo[] _contextInfo;

        public SDD1_PEM(SDD1_BG associatedBG0, SDD1_BG associatedBG1, SDD1_BG associatedBG2, SDD1_BG associatedBG3, SDD1_BG associatedBG4, SDD1_BG associatedBG5, SDD1_BG associatedBG6, SDD1_BG associatedBG7)
        {
            this._BG = new SDD1_BG[8];
            this._contextInfo = new SDD1_ContextInfo[32];

            this._BG[0] = associatedBG0;
            this._BG[1] = associatedBG1;
            this._BG[2] = associatedBG2;
            this._BG[3] = associatedBG3;
            this._BG[4] = associatedBG4;
            this._BG[5] = associatedBG5;
            this._BG[6] = associatedBG6;
            this._BG[7] = associatedBG7;
        }

        public byte GetBit(byte context)
        {
            bool endOfRun = false;
            byte bit;

            var pContInfo = this._contextInfo[context];
            var currStatus = pContInfo.status;
            var pState = evolution_table[currStatus];
            var currentMPS = pContInfo.MPS;

            bit = (this._BG[pState.code_num]).GetBit(ref endOfRun);

            if (endOfRun)
            {
                if (bit > 0)
                {
                    if ((currStatus & 0xFE) != 0xFE)
                        pContInfo.MPS ^= 0x01;
                    pContInfo.status = pState.nextIfLPS;
                }
                else
                    pContInfo.status = pState.nextIfMPS;
            }

            return Convert.ToByte(bit ^ currentMPS);
        }

        public void PrepareDecomp()
        {
            for (byte i = 0; i < 32; i++)
            {
                this._contextInfo[i].status = 0;
                this._contextInfo[i].MPS = 0;
            }
        }

        private struct SDD1_ContextInfo
        {
            public byte MPS;
            public byte status;
        }

        private struct State
        {
            public byte code_num;
            public byte nextIfLPS;
            public byte nextIfMPS;
        };
    }
}