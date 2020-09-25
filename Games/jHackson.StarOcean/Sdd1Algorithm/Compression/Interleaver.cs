// <copyright file="Interleaver.cs" company="BahaBulle">
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
    using System.Globalization;
    using System.Text;
    using NLog;

    public class Interleaver
    {
        private static readonly Logger Logger = LogManager.GetLogger("PluginSO");

        private readonly byte[] bitInd;
        private readonly int[] bytePtr;
        private readonly List<byte>[] codewBuffer;
        private readonly List<byte> codewSequence;
        private readonly StringBuilder logMessage = new StringBuilder();
        private byte oBitInd;

        public Interleaver(
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
            this.bytePtr = new int[8];
        }

        public void Launch()
        {
            foreach (var p in this.codewSequence)
            {
                if (this.MoveBit(p) > 0)
                {
                    for (byte i = 0; i < p; i++)
                    {
                        this.MoveBit(p);
                    }
                }
            }

            if (this.oBitInd > 0)
            {
                ++Sdd1.OutBufferLength;
            }
        }

        public void PrepareComp(byte header)
        {
            Sdd1.OutBufferLength = 0;
            Sdd1.SetByte(Sdd1.OutBufferPosition, Convert.ToByte((header << 4) & 0xFF));

            this.oBitInd = 4;

            for (byte i = 0; i < 8; i++)
            {
                this.bytePtr[i] = 0;
                this.bitInd[i] = 0;
            }
        }

        private byte MoveBit(byte code_num)
        {
            if (this.oBitInd == 0)
            {
                Sdd1.SetByte(Sdd1.OutBufferPosition, 0);
            }

            byte bit = Convert.ToByte(((this.codewBuffer[code_num][this.bytePtr[code_num]] & (0x80 >> this.bitInd[code_num])) << this.bitInd[code_num]) & 0xFF);

            Sdd1.SetByteOr(Sdd1.OutBufferPosition, Convert.ToByte((bit >> this.oBitInd) & 0xFF));
            this.logMessage.Append(string.Format(CultureInfo.InvariantCulture, "{0:X02} ", Convert.ToByte((bit >> this.oBitInd) & 0xFF)));

            if ((++this.bitInd[code_num] & 0x08) > 0)
            {
                this.bitInd[code_num] = 0;
                this.bytePtr[code_num]++;
            }

            if ((++this.oBitInd & 0x08) > 0)
            {
                this.logMessage.Append(string.Format(CultureInfo.InvariantCulture, "-> {0:X02}", Sdd1.GetByte(Sdd1.OutBufferPosition)));
                Logger.Debug(this.logMessage.ToString());
                this.logMessage.Clear();
                this.oBitInd = 0;
                Sdd1.OutBufferPosition++;
                ++Sdd1.OutBufferLength;
            }

            return bit;
        }
    }
}