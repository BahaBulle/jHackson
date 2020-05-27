/**********************************************************************

S-DD1'inverse algorithm emulation code
--------------------------------------

Author:      Andreas Naive
Date:        August 2003
Last update: October 2004

This code is Public Domain. There is no copyright holded by the author.
Said this, the author wish to explicitly emphasize his inalienable moral rights
over this piece of intelectual work and the previous research that made it
possible, as recognized by most of the copyright laws around the world.

This code is provided 'as-is', with no warranty, expressed or implied.
No responsability is assumed by the author in connection with it.

The author is greatly indebted with The Dumper, without whose help and
patience providing him with real S-DD1 data the research would have never been
possible. He also wish to note that in the very beggining of his research,
Neviksti had done some steps in the right direction. By last, the author is
indirectly indebted to all the people that worked and contributed in the
S-DD1 issue in the past.

An algorithm's documentation is available as a separate document.
The implementation is obvious when the algorithm is
understood.

*************************************************************************/

using jHackson.StarOcean.SDD1Algorithm.Common;
using System.Collections.Generic;

namespace jHackson.StarOcean.SDD1Algorithm.Compression
{
    // Probability Estimation Module Compression
    public class SDD1_PEMC
    {
        //private static readonly State[] evolution_table = new State[] {
        //    new State{ code_num = 0, nextIfMPS = 25, nextIfLPS = 25},
        //    new State{ code_num = 0, nextIfMPS =  2, nextIfLPS =  1},
        //    new State{ code_num = 0, nextIfMPS =  3, nextIfLPS =  1},
        //    new State{ code_num = 0, nextIfMPS =  4, nextIfLPS =  2},
        //    new State{ code_num = 0, nextIfMPS =  5, nextIfLPS =  3},
        //    new State{ code_num = 1, nextIfMPS =  6, nextIfLPS =  4},
        //    new State{ code_num = 1, nextIfMPS =  7, nextIfLPS =  5},
        //    new State{ code_num = 1, nextIfMPS =  8, nextIfLPS =  6},
        //    new State{ code_num = 1, nextIfMPS =  9, nextIfLPS =  7},
        //    new State{ code_num = 2, nextIfMPS = 10, nextIfLPS =  8},
        //    new State{ code_num = 2, nextIfMPS = 11, nextIfLPS =  9},
        //    new State{ code_num = 2, nextIfMPS = 12, nextIfLPS = 10},
        //    new State{ code_num = 2, nextIfMPS = 13, nextIfLPS = 11},
        //    new State{ code_num = 3, nextIfMPS = 14, nextIfLPS = 12},
        //    new State{ code_num = 3, nextIfMPS = 15, nextIfLPS = 13},
        //    new State{ code_num = 3, nextIfMPS = 16, nextIfLPS = 14},
        //    new State{ code_num = 3, nextIfMPS = 17, nextIfLPS = 15},
        //    new State{ code_num = 4, nextIfMPS = 18, nextIfLPS = 16},
        //    new State{ code_num = 4, nextIfMPS = 19, nextIfLPS = 17},
        //    new State{ code_num = 5, nextIfMPS = 20, nextIfLPS = 18},
        //    new State{ code_num = 5, nextIfMPS = 21, nextIfLPS = 19},
        //    new State{ code_num = 6, nextIfMPS = 22, nextIfLPS = 20},
        //    new State{ code_num = 6, nextIfMPS = 23, nextIfLPS = 21},
        //    new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 22},
        //    new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 23},
        //    new State{ code_num = 0, nextIfMPS = 26, nextIfLPS =  1},
        //    new State{ code_num = 1, nextIfMPS = 27, nextIfLPS =  2},
        //    new State{ code_num = 2, nextIfMPS = 28, nextIfLPS =  4},
        //    new State{ code_num = 3, nextIfMPS = 29, nextIfLPS =  8},
        //    new State{ code_num = 4, nextIfMPS = 30, nextIfLPS = 12},
        //    new State{ code_num = 5, nextIfMPS = 31, nextIfLPS = 16},
        //    new State{ code_num = 6, nextIfMPS = 32, nextIfLPS = 18},
        //    new State{ code_num = 7, nextIfMPS = 24, nextIfLPS = 22}
        //};
        private static readonly List<State> evolution_table = SDD1Helper.GetEvolutionTable();

        private readonly SDD1_ContextInfo[] _contextInfo;

        private readonly SDD1_CMC CM;
        private readonly SDD1_GCE GCE;
        private uint inputLength;

        public SDD1_PEMC(SDD1_CMC associatedCM, SDD1_GCE associatedGCE)
        {
            this.CM = associatedCM;
            this.GCE = associatedGCE;

            this.CM.PEM = this;

            this._contextInfo = new SDD1_ContextInfo[32];
        }

        public byte GetMPS(byte context)
        {
            return this._contextInfo[context].MPS;
        }

        public void Launch()
        {
            bool endOfRun = false;
            byte bit;
            byte context = 0;
            byte currStatus;
            State pState;

            for (int i = 0; i < this.inputLength; i++)
            {
                bit = this.CM.GetBit(ref context);

                currStatus = this._contextInfo[context].status;
                pState = evolution_table[currStatus];
                bit ^= this._contextInfo[context].MPS;

                this.GCE.PutBit(pState.code_num, bit, ref endOfRun);

                if (endOfRun)
                {
                    if (bit > 0)
                    {
                        if ((currStatus & 0xFE) == 0)
                            this._contextInfo[context].MPS ^= 0x01;

                        this._contextInfo[context].status = pState.nextIfLPS;
                    }
                    else
                        this._contextInfo[context].status = pState.nextIfMPS;
                }
            }

            this.GCE.FinishComp();
        }

        public void PrepareComp(byte header, ushort length)
        {
            for (byte i = 0; i < 32; i++)
            {
                this._contextInfo[i].status = 0;
                this._contextInfo[i].MPS = 0;
            }

            this.inputLength = length;

            if (((header & 0x0C) != 0x0C) && (length & 0x0001) > 0)
                this.inputLength++;

            this.inputLength <<= 3;
        }

        //private struct SDD1_ContextInfo
        //{
        //    public byte MPS;
        //    public byte status;
        //};

        //private struct State
        //{
        //    public byte code_num;
        //    public byte nextIfLPS;
        //    public byte nextIfMPS;
        //};
    }
}