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

using System.IO;

namespace jHackson.StarOcean.Compression
{
    public class SDD1_Decomp
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

        public SDD1_Decomp()
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

        public MemoryStream Decompress(MemoryStream in_buf, ushort out_len)
        {
            var out_buf = new MemoryStream();

            using (var stream = new BinaryReader(in_buf))
            {
                stream.BaseStream.Position = 0;

                IM.PrepareDecomp(stream);
                BG0.PrepareDecomp();
                BG1.PrepareDecomp();
                BG2.PrepareDecomp();
                BG3.PrepareDecomp();
                BG4.PrepareDecomp();
                BG5.PrepareDecomp();
                BG6.PrepareDecomp();
                BG7.PrepareDecomp();
                PEM.PrepareDecomp();
                CM.PrepareDecomp(stream);
                OL.PrepareDecomp(stream, out_len, out_buf);

                OL.Launch();
            }

            return out_buf;
        }
    }
}