// <copyright file="ContextModelCompression.cs" company="BahaBulle">
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

    public class ContextModelCompression
    {
        private readonly List<byte>[] bitplaneBuffer;
        private readonly byte[] bpBitInd;
        private readonly int?[] bytePtr;
        private readonly ushort[] prevBitplaneBits;
        private byte bitNumber;
        private byte bitplanesInfo;
        private byte contextBitsInfo;
        private byte currBitplane;

        public ContextModelCompression(
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
            this.prevBitplaneBits = new ushort[8];
            this.bytePtr = new int?[8];

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

        public ProbabilityEstimationModuleCompression ProbabilityEstimationModuleCompression { get; set; }

        public byte GetBit(ref byte context)
        {
            byte currContext;
            byte bit;

            switch (this.bitplanesInfo)
            {
                case 0x00:
                    this.currBitplane ^= 0x01;
                    break;

                case 0x04:
                    this.currBitplane ^= 0x01;
                    if ((this.bitNumber & 0x7F) == 0)
                    {
                        this.currBitplane = Convert.ToByte((this.currBitplane + 2) & 0x07 & 0xFF);
                    }

                    break;

                case 0x08:
                    this.currBitplane ^= 0x01;
                    if ((this.bitNumber & 0x7F) == 0)
                    {
                        this.currBitplane ^= 0x02;
                    }

                    break;

                case 0x0c:
                    this.currBitplane = Convert.ToByte(this.bitNumber & 0x07 & 0xFF);
                    break;
            }

            currContext = Convert.ToByte(((this.currBitplane & 0x01) << 4) & 0xFF);
            switch (this.contextBitsInfo)
            {
                case 0x00:
                    currContext |= Convert.ToByte(((this.prevBitplaneBits[this.currBitplane] & 0x01c0) >> 5) | this.prevBitplaneBits[this.currBitplane] & 0x0001 & 0xFF);
                    break;

                case 0x01:
                    currContext |= Convert.ToByte(((this.prevBitplaneBits[this.currBitplane] & 0x0180) >> 5) | this.prevBitplaneBits[this.currBitplane] & 0x0001 & 0xFF);
                    break;

                case 0x02:
                    currContext |= Convert.ToByte(((this.prevBitplaneBits[this.currBitplane] & 0x00c0) >> 5) | this.prevBitplaneBits[this.currBitplane] & 0x0001 & 0xFF);
                    break;

                case 0x03:
                    currContext |= Convert.ToByte(((this.prevBitplaneBits[this.currBitplane] & 0x0180) >> 5) | this.prevBitplaneBits[this.currBitplane] & 0x0003 & 0xFF);
                    break;
            }

            if (this.bytePtr[this.currBitplane].HasValue && this.bytePtr[this.currBitplane] == this.bitplaneBuffer[this.currBitplane].Count)
            {
                bit = this.ProbabilityEstimationModuleCompression.GetMPS(currContext);
            }
            else
            {
                bit = Convert.ToByte(this.bitplaneBuffer[this.currBitplane][this.bytePtr[this.currBitplane].Value] & (0x80 >> this.bpBitInd[this.currBitplane]) & 0xFF) > 0 ? (byte)1 : (byte)0;
                if (((++this.bpBitInd[this.currBitplane]) & 0x08) > 0)
                {
                    this.bpBitInd[this.currBitplane] = 0;
                    this.bytePtr[this.currBitplane]++;
                }
            }

            this.prevBitplaneBits[this.currBitplane] <<= 1;
            this.prevBitplaneBits[this.currBitplane] |= bit;

            this.bitNumber++;

            context = currContext;

            return bit;
        }

        public void PrepareComp(byte header)
        {
            this.bitplanesInfo = Convert.ToByte(header & 0x0C & 0xFF);
            this.contextBitsInfo = Convert.ToByte(header & 0x03 & 0xFF);

            for (int i = 0; i < 8; i++)
            {
                this.bytePtr[i] = this.bitplaneBuffer[i].Count > 0 ? (int?)0 : null;
                this.bpBitInd[i] = 0;
                this.prevBitplaneBits[i] = 0;
            }

            this.bitNumber = 0;

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
            }
        }
    }
}