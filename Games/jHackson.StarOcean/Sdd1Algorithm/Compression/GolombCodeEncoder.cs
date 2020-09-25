// <copyright file="GolombCodeEncoder.cs" company="BahaBulle">
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

    public class GolombCodeEncoder
    {
        private readonly byte[] bitInd;

        private readonly List<byte>[] codewBuffer;

        private readonly List<byte> codewSequence;

        private readonly byte[] mPScount;

        public GolombCodeEncoder(
            List<byte> associatedCWSeq,
            List<byte> associatedCWBuf0,
            List<byte> associatedCWBuf1,
            List<byte> associatedCWBuf2,
            List<byte> associatedCWBuf3,
            List<byte> associatedCWBuf4,
            List<byte> associatedCWBuf5,
            List<byte> associatedCWBuf6,
            List<byte> associatedCWBuf7)
        {
            this.codewSequence = associatedCWSeq;

            this.codewBuffer = new List<byte>[8];
            this.codewBuffer[0] = associatedCWBuf0;
            this.codewBuffer[1] = associatedCWBuf1;
            this.codewBuffer[2] = associatedCWBuf2;
            this.codewBuffer[3] = associatedCWBuf3;
            this.codewBuffer[4] = associatedCWBuf4;
            this.codewBuffer[5] = associatedCWBuf5;
            this.codewBuffer[6] = associatedCWBuf6;
            this.codewBuffer[7] = associatedCWBuf7;

            this.bitInd = new byte[8];
            this.mPScount = new byte[8];
        }

        public void FinishComp()
        {
            for (byte i = 0; i < 8; i++)
            {
                if (this.mPScount[i] > 0)
                {
                    this.OutputBit(i, false);
                }
            }
        }

        public void PrepareComp()
        {
            for (byte i = 0; i < 8; i++)
            {
                this.mPScount[i] = 0;
                this.bitInd[i] = 0;
            }
        }

        public void PutBit(byte codeNum, byte bit, ref bool endOfRun)
        {
            if (this.mPScount[codeNum] == 0)
            {
                this.codewSequence.Add(codeNum);
            }

            if (bit > 0)
            {
                endOfRun = true;
                this.OutputBit(codeNum, true);

                for (byte i = 0, aux = 0x01; i < codeNum; i++, aux <<= 1)
                {
                    this.OutputBit(codeNum, (this.mPScount[codeNum] & aux) == 0);
                }

                this.mPScount[codeNum] = 0;
            }
            else
            {
                if (++this.mPScount[codeNum] == (1 << codeNum))
                {
                    endOfRun = true;
                    this.OutputBit(codeNum, false);
                    this.mPScount[codeNum] = 0;
                }
                else
                {
                    endOfRun = false;
                }
            }
        }

        private void OutputBit(byte code_num, bool bit)
        {
            byte oBit = Convert.ToByte((bit ? 0x80 >> this.bitInd[code_num] : 0x00) & 0xFF);

            if (this.bitInd[code_num] == 0)
            {
                this.codewBuffer[code_num].Add(0);
            }

            this.codewBuffer[code_num][^1] |= oBit;

            ++this.bitInd[code_num];
            this.bitInd[code_num] &= 0x07;
        }
    }
}