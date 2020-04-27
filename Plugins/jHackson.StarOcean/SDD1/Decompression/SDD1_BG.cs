/************************************************************************

S-DD1'algorithm emulation code
------------------------------

Author:	     Andreas Naive
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

************************************************************************/

namespace jHackson.StarOcean.Compression
{
    // Bits Generator
    internal class SDD1_BG
    {
        private readonly byte code_num;

        private readonly SDD1_GCD GCD;

        private bool LPSind;

        private byte MPScount;

        public SDD1_BG(SDD1_GCD associatedGCD, byte code)
        {
            this.GCD = associatedGCD;
            this.code_num = code;
        }

        public byte GetBit(ref bool endOfRun)
        {
            byte bit;

            if (!(this.MPScount > 0 || this.LPSind))
                this.GCD.GetRunCount(this.code_num, ref this.MPScount, ref this.LPSind);

            if (this.MPScount > 0)
            {
                bit = 0;
                this.MPScount--;
            }
            else
            {
                bit = 1;
                this.LPSind = false;
            }

            if (this.MPScount > 0 || this.LPSind)
                endOfRun = false;
            else
                endOfRun = true;

            return bit;
        }

        public void PrepareDecomp()
        {
            this.MPScount = 0;
            this.LPSind = false;
        }
    }
}