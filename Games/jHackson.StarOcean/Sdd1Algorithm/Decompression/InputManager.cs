// <copyright file="InputManager.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

/************************************************************************

S-DD1'algorithm emulation code
------------------------------

Author: Andreas Naive
Date: August 2003
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

namespace JHackson.StarOcean.SDD1Algorithm.Decompression
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.StarOcean.Extensions;
    using NLog;

    internal class InputManager
    {
        private static readonly Logger Logger = LogManager.GetLogger("PluginSO");
        private byte bitCount;
        private BinaryReader bytePtr;

        public byte GetCodeword(byte code_len)
        {
            byte codeword;

            codeword = Convert.ToByte((this.bytePtr.PeekByte() << this.bitCount) & 0xFF);
            Logger.Debug(CultureInfo.InvariantCulture, "1 - {0} : {1:X02}", this.bitCount, this.bytePtr.PeekByte());

            ++this.bitCount;

            if ((codeword & 0x80) == 0x80)
            {
                codeword |= Convert.ToByte((this.bytePtr.PeekByte(1) >> (9 - this.bitCount)) & 0xFF);
                Logger.Debug(CultureInfo.InvariantCulture, "2 - {0} : {1:X02}", this.bitCount, this.bytePtr.PeekByte(1));
                this.bitCount += code_len;
            }

            if ((this.bitCount & 0x08) == 0x08)
            {
                this.bytePtr.ReadByte();
#pragma warning disable CA1303 // Ne pas passer de littéraux en paramètres localisés
                Logger.Debug(CultureInfo.InvariantCulture, "Position : {0}", this.bytePtr.BaseStream.Position);
#pragma warning restore CA1303 // Ne pas passer de littéraux en paramètres localisés
                this.bitCount &= 0x07;
            }

            return codeword;
        }

        public void PrepareDecomp(BinaryReader buffer)
        {
            this.bytePtr = buffer;
            this.bitCount = 4;
        }
    }
}