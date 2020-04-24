using System;
using System.IO;

namespace jHackson.StarOcean.Compression
{
    // Output Logic
    internal class SDD1_OL
    {
        private readonly SDD1_CM CM;
        private byte bitplanesInfo;
        private StreamWriter buffer;
        private ushort length;

        public SDD1_OL(SDD1_CM associatedCM)
        {
            this.CM = associatedCM;
        }

        public void Launch()
        {
            byte i;
            byte register1;
            byte register2 = 0;

            switch (this.bitplanesInfo)
            {
                case 0x00:
                case 0x40:
                case 0x80:
                    i = 1;
                    do
                    {
                        // if length == 0, we output 2^16 bytes
                        if (i == 0)
                        {
                            this.buffer.Write(register2);
                            i = Convert.ToByte(~i);
                        }
                        else
                        {
                            for (register1 = register2 = 0, i = 0x80; i > 0; i >>= 1)
                            {
                                if (this.CM.GetBit() > 0)
                                    register1 |= i;
                                if (this.CM.GetBit() > 0)
                                    register2 |= i;
                            }
                            this.buffer.Write(register1);
                        }
                    } while (--this.length > 0);
                    break;

                case 0xc0:
                    do
                    {
                        for (register1 = 0, i = 0x01; i > 0; i <<= 1)
                        {
                            if (this.CM.GetBit() > 0)
                                register1 |= i;
                        }
                        this.buffer.Write(register1);
                    } while (--this.length > 0);
                    break;
            }
        }

        public void PrepareDecomp(StreamReader first_byte, ushort out_len, StreamWriter out_buf)
        {
            this.bitplanesInfo = Convert.ToByte(first_byte.Peek() & 0xC0);
            this.length = out_len;
            this.buffer = out_buf;
        }
    }
}