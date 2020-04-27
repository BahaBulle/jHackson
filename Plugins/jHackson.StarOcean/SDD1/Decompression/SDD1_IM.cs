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

using System;
using System.IO;

namespace jHackson.StarOcean.Compression
{
    // Input Manager
    internal class SDD1_IM
    {
        private byte _bit_count;
        private StreamReader _byte_ptr;

        public byte GetCodeword(byte code_len)
        {
            byte codeword;

            var pos = this._byte_ptr.BaseStream.Position;

            codeword = Convert.ToByte(this._byte_ptr.Peek() << this._bit_count);

            ++this._bit_count;

            if ((codeword & 0x80) == 0x80)
            {
                this._byte_ptr.BaseStream.Position++;
                codeword |= Convert.ToByte(this._byte_ptr.Peek() >> (9 - this._bit_count));
                this._byte_ptr.BaseStream.Position = pos;
                this._bit_count += code_len;
            }

            if ((this._bit_count & 0x08) == 0x08)
            {
                this._byte_ptr.Read();
                this._bit_count &= 0x07;
            }

            return codeword;
        }

        public void PrepareDecomp(StreamReader buffer)
        {
            this._byte_ptr = buffer;
            this._bit_count = 4;
        }
    }
}