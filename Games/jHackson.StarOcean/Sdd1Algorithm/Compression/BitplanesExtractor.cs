// <copyright file="BitplanesExtractor.cs" company="BahaBulle">
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using JHackson.StarOcean.Extensions;

    public class BitplanesExtractor
    {
        private readonly List<byte>[] bitplaneBuffer;
        private readonly byte[] bpBitInd;

        private byte bitplanesInfo;
        private byte currBitplane;
        private byte inBitInd;
        private BinaryReader inputBuffer;
        private ushort inputLength;

        public BitplanesExtractor(
            List<byte> associatedBBuf0,
            List<byte> associatedBBuf1,
            List<byte> associatedBBuf2,
            List<byte> associatedBBuf3,
            List<byte> associatedBBuf4,
            List<byte> associatedBBuf5,
            List<byte> associatedBBuf6,
            List<byte> associatedBBuf7)
        {
            this.bpBitInd = new byte[8];

            this.bitplaneBuffer = new List<byte>[8];
            this.bitplaneBuffer[0] = associatedBBuf0;
            this.bitplaneBuffer[1] = associatedBBuf1;
            this.bitplaneBuffer[2] = associatedBBuf2;
            this.bitplaneBuffer[3] = associatedBBuf3;
            this.bitplaneBuffer[4] = associatedBBuf4;
            this.bitplaneBuffer[5] = associatedBBuf5;
            this.bitplaneBuffer[6] = associatedBBuf6;
            this.bitplaneBuffer[7] = associatedBBuf7;
        }

        public void Launch()
        {
            --this.inputLength;

            switch (this.bitplanesInfo)
            {
                case 0x00:
                    this.currBitplane = 1;
                    break;

                case 0x04:
                    this.currBitplane = 7;
                    break;

                case 0x08:
                    this.currBitplane = 3;
                    break;

                case 0x0C:
                    this.inBitInd = 7;
                    for (byte i = 0; i < 8; i++)
                    {
                        this.bpBitInd[i] = 0;
                    }

                    break;
            }

            ushort counter = 0;
            do
            {
                switch (this.bitplanesInfo)
                {
                    case 0x00:
                        this.currBitplane ^= 0x01;
                        this.bitplaneBuffer[this.currBitplane].Add(this.inputBuffer.ReadByte());
                        break;

                    case 0x04:
                        this.currBitplane ^= 0x01;
                        if ((counter & 0x000F) == 0)
                        {
                            this.currBitplane = Convert.ToByte((this.currBitplane + 2) & 0x07 & 0xFF);
                        }

                        this.bitplaneBuffer[this.currBitplane].Add(this.inputBuffer.ReadByte());
                        break;

                    case 0x08:
                        this.currBitplane ^= 0x01;
                        if ((counter & 0x000F) == 0)
                        {
                            this.currBitplane ^= 0x02;
                        }

                        this.bitplaneBuffer[this.currBitplane].Add(this.inputBuffer.ReadByte());
                        break;

                    case 0x0c:
                        for (byte i = 0; i < 8; i++)
                        {
                            this.PutBit(i);
                        }

                        this.inputBuffer.ReadByte();
                        break;
                }
            }
            while (counter++ < this.inputLength);
        }

        public void PrepareComp(BinaryReader bufferIn, byte header)
        {
            if (bufferIn == null)
            {
                throw new ArgumentNullException(nameof(bufferIn));
            }

            this.inputLength = (ushort)bufferIn.BaseStream.Length;
            this.inputBuffer = bufferIn;
            this.bitplanesInfo = Convert.ToByte((header & 0x0C) & 0xFF);
        }

        private void PutBit(byte bitplane)
        {
            if (this.bpBitInd[bitplane] == 0)
            {
                this.bitplaneBuffer[bitplane].Add(0);
            }

            this.bitplaneBuffer[bitplane][^1] |= Convert.ToByte((((this.inputBuffer.PeekByte() & (0x80 >> this.inBitInd)) << this.inBitInd) >> this.bpBitInd[bitplane]) & 0xFF);

            this.bpBitInd[bitplane]++;
            this.bpBitInd[bitplane] &= 0x07;

            this.inBitInd--;
            this.inBitInd &= 0x07;
        }
    }
}