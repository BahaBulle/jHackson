// <copyright file="SDD1.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.StarOcean.SDD1Algorithm.Compression;
    using JHackson.StarOcean.SDD1Algorithm.Decompression;

    public class SDD1 : ISDD1Comp, ISDD1Decomp
    {
        private SDD1_BE be;
        private SDD1_BG bg0;
        private SDD1_BG bg1;
        private SDD1_BG bg2;
        private SDD1_BG bg3;
        private SDD1_BG bg4;
        private SDD1_BG bg5;
        private SDD1_BG bg6;
        private SDD1_BG bg7;
        private List<byte>[] bitplaneBuffer;
        private SDD1_CMC cmc;
        private SDD1_CMD cmd;
        private List<byte>[] codewordBuffer;
        private List<byte> codewordsSequence;
        private SDD1_GCD gcd;
        private SDD1_GCE gce;
        private SDD1_I il;
        private SDD1_IM im;
        private SDD1_OL ol;
        private SDD1_PEMC pemc;
        private SDD1_PEMD pemd;

        public SDD1()
        {
        }

        public static byte[] OutBuffer { get; set; }

        public static int OutBufferLength { get; set; }

        public static int OutBufferPosition { get; set; }

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

                this.im.PrepareDecomp(stream);
                this.bg0.PrepareDecomp();
                this.bg1.PrepareDecomp();
                this.bg2.PrepareDecomp();
                this.bg3.PrepareDecomp();
                this.bg4.PrepareDecomp();
                this.bg5.PrepareDecomp();
                this.bg6.PrepareDecomp();
                this.bg7.PrepareDecomp();
                this.pemd.PrepareDecomp();
                this.cmd.PrepareDecomp(stream);
                this.ol.PrepareDecomp(stream, out_len, out_buf);

                this.ol.Launch();
            }

            return out_buf;
        }

        private void Compress(byte header, BinaryReader in_buf)
        {
            in_buf.BaseStream.Position = 0;
            OutBufferPosition = 0;

            // Step 1
            for (byte i = 0; i < 8; i++)
            {
                this.bitplaneBuffer[i].Clear();
            }

            this.be.PrepareComp(in_buf, header);
            this.be.Launch();

            // Step 2
            this.codewordsSequence.Clear();

            for (byte i = 0; i < 8; i++)
            {
                this.codewordBuffer[i].Clear();
            }

            this.cmc.PrepareComp(header);
            this.pemc.PrepareComp(header, (ushort)in_buf.BaseStream.Length);
            this.gce.PrepareComp();
            this.pemc.Launch();

            // Step 3
            this.il.PrepareComp(header);
            this.il.Launch();
        }

        private void InitCompression()
        {
            this.bitplaneBuffer = new List<byte>[8];
            this.codewordBuffer = new List<byte>[8];
            this.codewordsSequence = new List<byte>();

            for (int i = 0; i < 8; i++)
            {
                this.bitplaneBuffer[i] = new List<byte>();
            }

            for (int i = 0; i < 8; i++)
            {
                this.codewordBuffer[i] = new List<byte>();
            }

            this.be = new SDD1_BE(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.cmc = new SDD1_CMC(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.gce = new SDD1_GCE(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
            this.pemc = new SDD1_PEMC(this.cmc, this.gce);
            this.il = new SDD1_I(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
        }

        private void InitDecompression()
        {
            this.im = new SDD1_IM();
            this.gcd = new SDD1_GCD(this.im);
            this.bg0 = new SDD1_BG(this.gcd, 0);
            this.bg1 = new SDD1_BG(this.gcd, 1);
            this.bg2 = new SDD1_BG(this.gcd, 2);
            this.bg3 = new SDD1_BG(this.gcd, 3);
            this.bg4 = new SDD1_BG(this.gcd, 4);
            this.bg5 = new SDD1_BG(this.gcd, 5);
            this.bg6 = new SDD1_BG(this.gcd, 6);
            this.bg7 = new SDD1_BG(this.gcd, 7);
            this.pemd = new SDD1_PEMD(this.bg0, this.bg1, this.bg2, this.bg3, this.bg4, this.bg5, this.bg6, this.bg7);
            this.cmd = new SDD1_CMD(this.pemd);
            this.ol = new SDD1_OL(this.cmd);
        }
    }
}