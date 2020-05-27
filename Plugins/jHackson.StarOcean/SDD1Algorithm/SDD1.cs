using jHackson.StarOcean.SDD1Algorithm.Compression;
using jHackson.StarOcean.SDD1Algorithm.Decompression;
using System.Collections.Generic;
using System.IO;

namespace jHackson.StarOcean.SDD1Algorithm
{
    public class SDD1 : ISDD1Comp, ISDD1Decomp
    {
        public static byte[] OutBuffer;
        public static int OutBufferLength;
        public static int OutBufferPosition;
        private SDD1_BE _BE;
        private List<byte>[] _bitplaneBuffer;
        private SDD1_CMC _CMC;
        private List<byte>[] _codewordBuffer;
        private List<byte> _codewordsSequence;
        private SDD1_GCE _GCE;
        private SDD1_I _I;
        private SDD1_PEMC _PEMC;
        private SDD1_BG BG0;
        private SDD1_BG BG1;
        private SDD1_BG BG2;
        private SDD1_BG BG3;
        private SDD1_BG BG4;
        private SDD1_BG BG5;
        private SDD1_BG BG6;
        private SDD1_BG BG7;
        private SDD1_CMD CMD;
        private SDD1_GCD GCD;
        private SDD1_IM IM;
        private SDD1_OL OL;
        private SDD1_PEMD PEMD;

        public SDD1()
        {
        }

        public MemoryStream Compress(MemoryStream in_buf)
        {
            this.InitCompression();

            OutBuffer = new byte[in_buf.Length];

            using (var stream = new BinaryReader(in_buf))
            {
                int min_length;
                byte[] buffer;

                this.Compress(0, stream);

                min_length = OutBufferLength;
                buffer = new byte[OutBufferLength];
                for (int i = 0; i < OutBufferLength; i++)
                {
                    buffer[i] = OutBuffer[i];
                }

                for (byte j = 1; j < 16; j++)
                {
                    this.Compress(j, stream);

                    if (OutBufferLength < min_length)
                    {
                        min_length = OutBufferLength;

                        for (int i = 0; i < OutBufferLength; i++)
                        {
                            buffer[i] = OutBuffer[i];
                        }
                    }
                }

                if (min_length < OutBufferLength)
                {
                    OutBufferLength = min_length;

                    for (int i = 0; i < min_length; i++)
                    {
                        OutBuffer[i] = buffer[i];
                    }
                }
            }

            return new MemoryStream(OutBuffer, 0, OutBufferLength);
        }

        public MemoryStream Decompress(MemoryStream in_buf, ushort out_len)
        {
            this.InitDecompression();

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
                PEMD.PrepareDecomp();
                CMD.PrepareDecomp(stream);
                OL.PrepareDecomp(stream, out_len, out_buf);

                OL.Launch();
            }

            return out_buf;
        }

        private void Compress(byte header, BinaryReader in_buf)
        {
            in_buf.BaseStream.Position = 0;
            OutBufferPosition = 0;

            //Step 1
            for (byte i = 0; i < 8; i++)
            {
                this._bitplaneBuffer[i].Clear();
            }

            this._BE.PrepareComp(in_buf, header);
            this._BE.Launch();

            //Step 2
            this._codewordsSequence.Clear();

            for (byte i = 0; i < 8; i++)
            {
                this._codewordBuffer[i].Clear();
            }

            this._CMC.PrepareComp(header);
            this._PEMC.PrepareComp(header, (ushort)in_buf.BaseStream.Length);
            this._GCE.PrepareComp();
            this._PEMC.Launch();

            //Step 3
            this._I.PrepareComp(header);
            this._I.Launch();
        }

        private void InitCompression()
        {
            this._bitplaneBuffer = new List<byte>[8];
            this._codewordBuffer = new List<byte>[8];
            this._codewordsSequence = new List<byte>();

            for (int i = 0; i < 8; i++)
            {
                this._bitplaneBuffer[i] = new List<byte>();
            }

            for (int i = 0; i < 8; i++)
            {
                this._codewordBuffer[i] = new List<byte>();
            }

            this._BE = new SDD1_BE(this._bitplaneBuffer[0], this._bitplaneBuffer[1], this._bitplaneBuffer[2], this._bitplaneBuffer[3], this._bitplaneBuffer[4], this._bitplaneBuffer[5], this._bitplaneBuffer[6], this._bitplaneBuffer[7]);
            this._CMC = new SDD1_CMC(this._bitplaneBuffer[0], this._bitplaneBuffer[1], this._bitplaneBuffer[2], this._bitplaneBuffer[3], this._bitplaneBuffer[4], this._bitplaneBuffer[5], this._bitplaneBuffer[6], this._bitplaneBuffer[7]);
            this._GCE = new SDD1_GCE(this._codewordsSequence, this._codewordBuffer[0], this._codewordBuffer[1], this._codewordBuffer[2], this._codewordBuffer[3], this._codewordBuffer[4], this._codewordBuffer[5], this._codewordBuffer[6], this._codewordBuffer[7]);
            this._PEMC = new SDD1_PEMC(this._CMC, this._GCE);
            this._I = new SDD1_I(this._codewordsSequence, this._codewordBuffer[0], this._codewordBuffer[1], this._codewordBuffer[2], this._codewordBuffer[3], this._codewordBuffer[4], this._codewordBuffer[5], this._codewordBuffer[6], this._codewordBuffer[7]);
        }

        private void InitDecompression()
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
            PEMD = new SDD1_PEMD(BG0, BG1, BG2, BG3, BG4, BG5, BG6, BG7);
            CMD = new SDD1_CMD(PEMD);
            OL = new SDD1_OL(CMD);
        }
    }
}