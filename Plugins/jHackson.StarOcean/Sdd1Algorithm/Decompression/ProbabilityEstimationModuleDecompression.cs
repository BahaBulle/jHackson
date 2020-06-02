// <copyright file="ProbabilityEstimationModuleDecompression.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

/************************************************************************

S-DD1'algorithm emulation code
------------------------------

Author: Andreas Naive
Date: August 2003
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

************************************************************************/

namespace JHackson.StarOcean.SDD1Algorithm.Decompression
{
    using System;
    using System.Collections.Generic;
    using JHackson.StarOcean.SDD1Algorithm.Common;

    internal class ProbabilityEstimationModuleDecompression
    {
        private static readonly List<State> EvolutionTable = Sdd1Helper.GetEvolutionTable();

        private readonly BitsGenerator[] bg;

        private readonly ContextInfo[] contextInfo;

        public ProbabilityEstimationModuleDecompression(BitsGenerator associatedBG0, BitsGenerator associatedBG1, BitsGenerator associatedBG2, BitsGenerator associatedBG3, BitsGenerator associatedBG4, BitsGenerator associatedBG5, BitsGenerator associatedBG6, BitsGenerator associatedBG7)
        {
            this.bg = new BitsGenerator[8];
            this.contextInfo = new ContextInfo[32];

            this.bg[0] = associatedBG0;
            this.bg[1] = associatedBG1;
            this.bg[2] = associatedBG2;
            this.bg[3] = associatedBG3;
            this.bg[4] = associatedBG4;
            this.bg[5] = associatedBG5;
            this.bg[6] = associatedBG6;
            this.bg[7] = associatedBG7;
        }

        public byte GetBit(byte context)
        {
            bool endOfRun = false;
            byte bit;

            var currStatus = this.contextInfo[context].Status;
            var pState = EvolutionTable[currStatus];
            var currentMPS = this.contextInfo[context].MPS;

            bit = this.bg[pState.CodeNum].GetBit(ref endOfRun);

            if (endOfRun)
            {
                if (bit > 0)
                {
                    if ((currStatus & 0xFE) == 0)
                    {
                        this.contextInfo[context].MPS ^= 0x01;
                    }

                    this.contextInfo[context].Status = pState.NextIfLPS;
                }
                else
                {
                    this.contextInfo[context].Status = pState.NextIfMPS;
                }
            }

            return Convert.ToByte(bit ^ currentMPS);
        }

        public void PrepareDecomp()
        {
            for (byte i = 0; i < 32; i++)
            {
                this.contextInfo[i].Status = 0;
                this.contextInfo[i].MPS = 0;
            }
        }
    }
}