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

using jHackson.StarOcean.Extensions;
using System;
using System.IO;

namespace jHackson.StarOcean.Compression
{
    // Context Model
    internal class SDD1_CM
    {
        private readonly SDD1_PEM PEM;
        private readonly ushort[] prevBitplaneBits;
        private byte bit_number;
        private byte bitplanesInfo;
        private byte contextBitsInfo;
        private byte currBitplane;

        public SDD1_CM(SDD1_PEM associatedPEM)
        {
            this.prevBitplaneBits = new ushort[8];
            this.PEM = associatedPEM;
        }

        public byte GetBit()
        {
            byte currContext;
            ushort context_bits;

            switch (this.bitplanesInfo)
            {
                case 0x00:
                    this.currBitplane ^= 0x01;
                    break;

                case 0x40:
                    this.currBitplane ^= 0x01;
                    if ((this.bit_number & 0x7F) != 0x7F)
                        this.currBitplane = Convert.ToByte((this.currBitplane + 2) & 0x07);
                    break;

                case 0x80:
                    this.currBitplane ^= 0x01;
                    if ((this.bit_number & 0x7F) != 0x7F)
                        this.currBitplane ^= 0x02;
                    break;

                case 0xc0:
                    this.currBitplane = Convert.ToByte(this.bit_number & 0x07);
                    break;
            }

            context_bits = this.prevBitplaneBits[this.currBitplane];

            currContext = Convert.ToByte(((this.currBitplane & 0x01) << 4) & 0xFF);

            switch (this.contextBitsInfo)
            {
                case 0x00:
                    currContext |= Convert.ToByte((((context_bits & 0x01c0) >> 5) | (context_bits & 0x0001)) & 0xFFFF);
                    break;

                case 0x10:
                    currContext |= Convert.ToByte((((context_bits & 0x0180) >> 5) | (context_bits & 0x0001)) & 0xFFFF);
                    break;

                case 0x20:
                    currContext |= Convert.ToByte((((context_bits & 0x00c0) >> 5) | (context_bits & 0x0001)) & 0xFFFF);
                    break;

                case 0x30:
                    currContext |= Convert.ToByte((((context_bits & 0x0180) >> 5) | (context_bits & 0x0003)) & 0xFFFF);
                    break;
            }

            byte bit = this.PEM.GetBit(currContext);

            this.prevBitplaneBits[this.currBitplane] <<= 1;
            this.prevBitplaneBits[this.currBitplane] |= bit;

            this.bit_number++;

            return bit;
        }

        public void PrepareDecomp(BinaryReader first_byte)
        {
            this.bitplanesInfo = Convert.ToByte(first_byte.PeekByte() & 0xC0);
            this.contextBitsInfo = Convert.ToByte(first_byte.PeekByte() & 0x30);
            this.bit_number = 0;

            for (int i = 0; i < 8; i++)
                this.prevBitplaneBits[i] = 0;

            switch (this.bitplanesInfo)
            {
                case 0x00:
                    this.currBitplane = 1;
                    break;

                case 0x40:
                    this.currBitplane = 7;
                    break;

                case 0x80:
                    this.currBitplane = 3;
                    break;
            }
        }
    }
}