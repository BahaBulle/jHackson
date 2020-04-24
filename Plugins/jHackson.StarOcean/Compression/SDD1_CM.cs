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

            currContext = Convert.ToByte((this.currBitplane & 0x01) << 4);

            switch (this.contextBitsInfo)
            {
                case 0x00:
                    currContext |= Convert.ToByte(((context_bits & 0x01c0) >> 5) | (context_bits & 0x0001));
                    break;

                case 0x10:
                    currContext |= Convert.ToByte(((context_bits & 0x0180) >> 5) | (context_bits & 0x0001));
                    break;

                case 0x20:
                    currContext |= Convert.ToByte(((context_bits & 0x00c0) >> 5) | (context_bits & 0x0001));
                    break;

                case 0x30:
                    currContext |= Convert.ToByte(((context_bits & 0x0180) >> 5) | (context_bits & 0x0003));
                    break;
            }

            byte bit = this.PEM.GetBit(currContext);

            context_bits <<= 1;
            context_bits |= bit;

            this.bit_number++;

            return bit;
        }

        public void PrepareDecomp(StreamReader first_byte)
        {
            this.bitplanesInfo = Convert.ToByte(first_byte.Peek() & 0xC0);
            this.contextBitsInfo = Convert.ToByte(first_byte.Peek() & 0x30);
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