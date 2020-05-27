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

using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace jHackson.StarOcean.SDD1Algorithm.Compression
{
    // Interleaver
    public class SDD1_I
    {
        private static readonly Logger _logger = LogManager.GetLogger("PluginSO");

        private readonly byte[] _bitInd;
        private readonly int[] _byte_ptr;
        private readonly List<byte>[] _codewBuffer;
        private readonly List<byte> _codewSequence;
        private byte _oBitInd;
        private StringBuilder logMessage = new StringBuilder();

        public SDD1_I(List<byte> associatedCWSeq, List<byte> associatedCWBuf0, List<byte> associatedCWBuf1, List<byte> associatedCWBuf2, List<byte> associatedCWBuf3,
                                                  List<byte> associatedCWBuf4, List<byte> associatedCWBuf5, List<byte> associatedCWBuf6, List<byte> associatedCWBuf7)
        {
            this._codewSequence = associatedCWSeq;

            this._codewBuffer = new List<byte>[8];
            this._codewBuffer[0] = associatedCWBuf0;
            this._codewBuffer[1] = associatedCWBuf1;
            this._codewBuffer[2] = associatedCWBuf2;
            this._codewBuffer[3] = associatedCWBuf3;
            this._codewBuffer[4] = associatedCWBuf4;
            this._codewBuffer[5] = associatedCWBuf5;
            this._codewBuffer[6] = associatedCWBuf6;
            this._codewBuffer[7] = associatedCWBuf7;

            this._bitInd = new byte[8];
            this._byte_ptr = new int[8];
        }

        public void Launch()
        {
            foreach (var p in this._codewSequence)
            {
                if (this.MoveBit(p) > 0)
                {
                    for (byte i = 0; i < p; i++)
                    {
                        this.MoveBit(p);
                    }
                }
            }

            if (_oBitInd > 0)
            {
                ++SDD1.OutBufferLength;
            }
        }

        public void PrepareComp(byte header)
        {
            SDD1.OutBufferLength = 0;
            SDD1.OutBuffer[SDD1.OutBufferPosition] = Convert.ToByte((header << 4) & 0xFF);

            this._oBitInd = 4;

            for (byte i = 0; i < 8; i++)
            {
                this._byte_ptr[i] = 0;
                //this._byte_ptr[i] = this._codewBuffer[i].First();
                this._bitInd[i] = 0;
            }
        }

        private byte MoveBit(byte code_num)
        {
            if (this._oBitInd == 0)
                SDD1.OutBuffer[SDD1.OutBufferPosition] = 0;

            byte bit = Convert.ToByte(((this._codewBuffer[code_num][this._byte_ptr[code_num]] & (0x80 >> this._bitInd[code_num])) << this._bitInd[code_num]) & 0xFF);

            SDD1.OutBuffer[SDD1.OutBufferPosition] |= Convert.ToByte((bit >> this._oBitInd) & 0xFF);
            logMessage.Append(string.Format("{0:X02} ", Convert.ToByte((bit >> this._oBitInd) & 0xFF)));

            if ((++this._bitInd[code_num] & 0x08) > 0)
            {
                this._bitInd[code_num] = 0;
                this._byte_ptr[code_num]++;
            }

            if ((++this._oBitInd & 0x08) > 0)
            {
                logMessage.Append(string.Format("-> {0:X02}", SDD1.OutBuffer[SDD1.OutBufferPosition]));
                _logger.Debug(logMessage.ToString());
                logMessage.Clear();
                this._oBitInd = 0;
                SDD1.OutBufferPosition++;
                ++SDD1.OutBufferLength;
            }

            return bit;
        }
    }
}