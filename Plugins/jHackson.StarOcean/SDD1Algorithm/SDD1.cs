// <copyright file="Sdd1.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.StarOcean.SDD1Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using JHackson.StarOcean.SDD1Algorithm.Compression;
    using JHackson.StarOcean.SDD1Algorithm.Decompression;

    public class Sdd1 : ISdd1Comp, ISdd1Decomp
    {
        private BitplanesExtractor be;
        private BitsGenerator bg0;
        private BitsGenerator bg1;
        private BitsGenerator bg2;
        private BitsGenerator bg3;
        private BitsGenerator bg4;
        private BitsGenerator bg5;
        private BitsGenerator bg6;
        private BitsGenerator bg7;
        private List<byte>[] bitplaneBuffer;
        private ContextModelCompression cmc;
        private ContextModelDecompression cmd;
        private List<byte>[] codewordBuffer;
        private List<byte> codewordsSequence;
        private GolombCodeDecoder gcd;
        private GolombCodeEncoder gce;
        private Interleaver il;
        private InputManager im;
        private OutputLogic ol;
        private ProbabilityEstimationModuleCompression pemc;
        private ProbabilityEstimationModuleDecompression pemd;

        public Sdd1()
        {
        }

        public static byte[] OutBuffer { get; set; }

        public static int OutBufferLength { get; set; }

        public static int OutBufferPosition { get; set; }

        public MemoryStream Compress(MemoryStream in_buf)
        {
            if (in_buf == null)
            {
                throw new ArgumentNullException(nameof(in_buf));
            }

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

            this.be = new BitplanesExtractor(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.cmc = new ContextModelCompression(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.gce = new GolombCodeEncoder(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
            this.pemc = new ProbabilityEstimationModuleCompression(this.cmc, this.gce);
            this.il = new Interleaver(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
        }

        private void InitDecompression()
        {
            this.im = new InputManager();
            this.gcd = new GolombCodeDecoder(this.im);
            this.bg0 = new BitsGenerator(this.gcd, 0);
            this.bg1 = new BitsGenerator(this.gcd, 1);
            this.bg2 = new BitsGenerator(this.gcd, 2);
            this.bg3 = new BitsGenerator(this.gcd, 3);
            this.bg4 = new BitsGenerator(this.gcd, 4);
            this.bg5 = new BitsGenerator(this.gcd, 5);
            this.bg6 = new BitsGenerator(this.gcd, 6);
            this.bg7 = new BitsGenerator(this.gcd, 7);
            this.pemd = new ProbabilityEstimationModuleDecompression(this.bg0, this.bg1, this.bg2, this.bg3, this.bg4, this.bg5, this.bg6, this.bg7);
            this.cmd = new ContextModelDecompression(this.pemd);
            this.ol = new OutputLogic(this.cmd);
        }
    }
}