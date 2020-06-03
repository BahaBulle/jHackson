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
        private static byte[] outBuffer;
        private List<byte>[] bitplaneBuffer;
        private BitplanesExtractor bitplanesExtractor;
        private BitsGenerator bitsGenerator0;
        private BitsGenerator bitsGenerator1;
        private BitsGenerator bitsGenerator2;
        private BitsGenerator bitsGenerator3;
        private BitsGenerator bitsGenerator4;
        private BitsGenerator bitsGenerator5;
        private BitsGenerator bitsGenerator6;
        private BitsGenerator bitsGenerator7;
        private List<byte>[] codewordBuffer;
        private List<byte> codewordsSequence;
        private ContextModelCompression contextModelCompression;
        private ContextModelDecompression contextModelDecompression;
        private GolombCodeDecoder golombCodeDecoder;
        private GolombCodeEncoder golombCodeEncoder;
        private InputManager inputManager;
        private Interleaver interleaver;
        private OutputLogic outputLogic;
        private ProbabilityEstimationModuleCompression probabilityEstimationModuleCompression;
        private ProbabilityEstimationModuleDecompression probabilityEstimationModuleDecompression;

        public Sdd1()
        {
        }

        public static int OutBufferLength { get; set; }

        public static int OutBufferPosition { get; set; }

        public static byte GetByte(int position)
        {
            return outBuffer[position];
        }

        public static void SetByte(int position, byte value)
        {
            outBuffer[position] = value;
        }

        public static void SetByteOr(int position, byte value)
        {
            outBuffer[position] |= value;
        }

        public MemoryStream Compress(MemoryStream in_buf)
        {
            if (in_buf == null)
            {
                throw new ArgumentNullException(nameof(in_buf));
            }

            this.InitCompression();

            outBuffer = new byte[in_buf.Length];

            using (var stream = new BinaryReader(in_buf))
            {
                int min_length;
                byte[] buffer;

                this.Compress(0, stream);

                min_length = OutBufferLength;
                buffer = new byte[OutBufferLength];
                for (int i = 0; i < OutBufferLength; i++)
                {
                    buffer[i] = outBuffer[i];
                }

                for (byte j = 1; j < 16; j++)
                {
                    this.Compress(j, stream);

                    if (OutBufferLength < min_length)
                    {
                        min_length = OutBufferLength;

                        for (int i = 0; i < OutBufferLength; i++)
                        {
                            buffer[i] = outBuffer[i];
                        }
                    }
                }

                if (min_length < OutBufferLength)
                {
                    OutBufferLength = min_length;

                    for (int i = 0; i < min_length; i++)
                    {
                        outBuffer[i] = buffer[i];
                    }
                }
            }

            return new MemoryStream(outBuffer, 0, OutBufferLength);
        }

        public MemoryStream Decompress(MemoryStream in_buf, ushort out_len)
        {
            this.InitDecompression();

            var out_buf = new MemoryStream();

            using (var stream = new BinaryReader(in_buf))
            {
                stream.BaseStream.Position = 0;

                this.inputManager.PrepareDecomp(stream);
                this.bitsGenerator0.PrepareDecomp();
                this.bitsGenerator1.PrepareDecomp();
                this.bitsGenerator2.PrepareDecomp();
                this.bitsGenerator3.PrepareDecomp();
                this.bitsGenerator4.PrepareDecomp();
                this.bitsGenerator5.PrepareDecomp();
                this.bitsGenerator6.PrepareDecomp();
                this.bitsGenerator7.PrepareDecomp();
                this.probabilityEstimationModuleDecompression.PrepareDecomp();
                this.contextModelDecompression.PrepareDecomp(stream);
                this.outputLogic.PrepareDecomp(stream, out_len, out_buf);

                this.outputLogic.Launch();
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

            this.bitplanesExtractor.PrepareComp(in_buf, header);
            this.bitplanesExtractor.Launch();

            // Step 2
            this.codewordsSequence.Clear();

            for (byte i = 0; i < 8; i++)
            {
                this.codewordBuffer[i].Clear();
            }

            this.contextModelCompression.PrepareComp(header);
            this.probabilityEstimationModuleCompression.PrepareComp(header, (ushort)in_buf.BaseStream.Length);
            this.golombCodeEncoder.PrepareComp();
            this.probabilityEstimationModuleCompression.Launch();

            // Step 3
            this.interleaver.PrepareComp(header);
            this.interleaver.Launch();
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

            this.bitplanesExtractor = new BitplanesExtractor(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.contextModelCompression = new ContextModelCompression(this.bitplaneBuffer[0], this.bitplaneBuffer[1], this.bitplaneBuffer[2], this.bitplaneBuffer[3], this.bitplaneBuffer[4], this.bitplaneBuffer[5], this.bitplaneBuffer[6], this.bitplaneBuffer[7]);
            this.golombCodeEncoder = new GolombCodeEncoder(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
            this.probabilityEstimationModuleCompression = new ProbabilityEstimationModuleCompression(this.contextModelCompression, this.golombCodeEncoder);
            this.interleaver = new Interleaver(this.codewordsSequence, this.codewordBuffer[0], this.codewordBuffer[1], this.codewordBuffer[2], this.codewordBuffer[3], this.codewordBuffer[4], this.codewordBuffer[5], this.codewordBuffer[6], this.codewordBuffer[7]);
        }

        private void InitDecompression()
        {
            this.inputManager = new InputManager();
            this.golombCodeDecoder = new GolombCodeDecoder(this.inputManager);
            this.bitsGenerator0 = new BitsGenerator(this.golombCodeDecoder, 0);
            this.bitsGenerator1 = new BitsGenerator(this.golombCodeDecoder, 1);
            this.bitsGenerator2 = new BitsGenerator(this.golombCodeDecoder, 2);
            this.bitsGenerator3 = new BitsGenerator(this.golombCodeDecoder, 3);
            this.bitsGenerator4 = new BitsGenerator(this.golombCodeDecoder, 4);
            this.bitsGenerator5 = new BitsGenerator(this.golombCodeDecoder, 5);
            this.bitsGenerator6 = new BitsGenerator(this.golombCodeDecoder, 6);
            this.bitsGenerator7 = new BitsGenerator(this.golombCodeDecoder, 7);
            this.probabilityEstimationModuleDecompression = new ProbabilityEstimationModuleDecompression(this.bitsGenerator0, this.bitsGenerator1, this.bitsGenerator2, this.bitsGenerator3, this.bitsGenerator4, this.bitsGenerator5, this.bitsGenerator6, this.bitsGenerator7);
            this.contextModelDecompression = new ContextModelDecompression(this.probabilityEstimationModuleDecompression);
            this.outputLogic = new OutputLogic(this.contextModelDecompression);
        }
    }
}