// <copyright file="SDD1_PEMC.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

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

namespace JHackson.StarOcean.SDD1Algorithm.Compression
{
    using System.Collections.Generic;
    using JHackson.StarOcean.SDD1Algorithm.Common;

    // Probability Estimation Module Compression
    public class SDD1_PEMC
    {
        private static readonly List<State> EvolutionTable = SDD1Helper.GetEvolutionTable();

        private readonly SDD1_CMC cm;
        private readonly SDD1_ContextInfo[] contextInfo;
        private readonly SDD1_GCE gce;
        private uint inputLength;

        public SDD1_PEMC(SDD1_CMC associatedCM, SDD1_GCE associatedGCE)
        {
            this.cm = associatedCM;
            this.gce = associatedGCE;

            this.cm.ProbabilityEstimationModuleCompression = this;

            this.contextInfo = new SDD1_ContextInfo[32];
        }

        public byte GetMPS(byte context)
        {
            return this.contextInfo[context].MPS;
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
                bit = this.cm.GetBit(ref context);

                currStatus = this.contextInfo[context].Status;
                pState = EvolutionTable[currStatus];
                bit ^= this.contextInfo[context].MPS;

                this.gce.PutBit(pState.CodeNum, bit, ref endOfRun);

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
            }

            this.gce.FinishComp();
        }

        public void PrepareComp(byte header, ushort length)
        {
            for (byte i = 0; i < 32; i++)
            {
                this.contextInfo[i].Status = 0;
                this.contextInfo[i].MPS = 0;
            }

            this.inputLength = length;

            if (((header & 0x0C) != 0x0C) && (length & 0x0001) > 0)
            {
                this.inputLength++;
            }

            this.inputLength <<= 3;
        }
    }
}