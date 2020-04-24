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