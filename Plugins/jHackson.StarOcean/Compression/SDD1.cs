using System.IO;

namespace jHackson.StarOcean.Compression
{
    public class SDD1
    {
        private readonly SDD1_BG BG0;
        private readonly SDD1_BG BG1;
        private readonly SDD1_BG BG2;
        private readonly SDD1_BG BG3;
        private readonly SDD1_BG BG4;
        private readonly SDD1_BG BG5;
        private readonly SDD1_BG BG6;
        private readonly SDD1_BG BG7;
        private readonly SDD1_CM CM;
        private readonly SDD1_GCD GCD;
        private readonly SDD1_IM IM;
        private readonly SDD1_OL OL;
        private readonly SDD1_PEM PEM;

        public SDD1()
        {
            IM = new SDD1_IM();
            GCD = new SDD1_GCD(IM);
            BG0 = new SDD1_BG(GCD, 0);
            BG1 = new SDD1_BG(GCD, 1);
            BG2 = new SDD1_BG(GCD, 2);
            BG3 = new SDD1_BG(GCD, 3);
            BG4 = new SDD1_BG(GCD, 4);
            BG5 = new SDD1_BG(GCD, 5);
            BG6 = new SDD1_BG(GCD, 6);
            BG7 = new SDD1_BG(GCD, 7);
            PEM = new SDD1_PEM(BG0, BG1, BG2, BG3, BG4, BG5, BG6, BG7);
            CM = new SDD1_CM(PEM);
            OL = new SDD1_OL(CM);
        }

        public void Decompress(StreamReader in_buf, ushort out_len, StreamWriter out_buf)
        {
            IM.PrepareDecomp(in_buf);
            BG0.PrepareDecomp();
            BG1.PrepareDecomp();
            BG2.PrepareDecomp();
            BG3.PrepareDecomp();
            BG4.PrepareDecomp();
            BG5.PrepareDecomp();
            BG6.PrepareDecomp();
            BG7.PrepareDecomp();
            PEM.PrepareDecomp();
            CM.PrepareDecomp(in_buf);
            OL.PrepareDecomp(in_buf, out_len, out_buf);

            OL.Launch();
        }
    }
}