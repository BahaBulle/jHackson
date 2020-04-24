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