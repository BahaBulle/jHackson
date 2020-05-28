// <copyright file="SDD1_BG.cs" company="BahaBulle">
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
    // Bits Generator
    internal class SDD1_BG
    {
        private readonly byte codeNum;

        private readonly SDD1_GCD gcd;

        private bool lpsInd;

        private byte mpsCount;

        public SDD1_BG(SDD1_GCD associatedGCD, byte code)
        {
            this.gcd = associatedGCD;
            this.codeNum = code;
        }

        public byte GetBit(ref bool endOfRun)
        {
            byte bit;

            if (!(this.mpsCount > 0 || this.lpsInd))
            {
                this.gcd.GetRunCount(this.codeNum, ref this.mpsCount, ref this.lpsInd);
            }

            if (this.mpsCount > 0)
            {
                bit = 0;
                this.mpsCount--;
            }
            else
            {
                bit = 1;
                this.lpsInd = false;
            }

            if (this.mpsCount > 0 || this.lpsInd)
            {
                endOfRun = false;
            }
            else
            {
                endOfRun = true;
            }

            return bit;
        }

        public void PrepareDecomp()
        {
            this.mpsCount = 0;
            this.lpsInd = false;
        }
    }
}