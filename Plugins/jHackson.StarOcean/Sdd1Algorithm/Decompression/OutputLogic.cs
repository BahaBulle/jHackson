// <copyright file="OutputLogic.cs" company="BahaBulle">
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
    using System.IO;
    using JHackson.StarOcean.Extensions;
    using NLog;

    internal class OutputLogic
    {
        private static readonly Logger Logger = LogManager.GetLogger("PluginSO");

        private readonly ContextModelDecompression cm;
        private byte bitplanesInfo;
        private MemoryStream buffer;
        private ushort length;

        public OutputLogic(ContextModelDecompression associatedCM)
        {
            this.cm = associatedCM;
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
                            this.buffer.WriteByte(register2);
                            Logger.Debug("Write 1 : {0:X02}", register2);
                            i = Convert.ToByte(~i & 0xFF);
                        }
                        else
                        {
                            for (register1 = register2 = 0, i = 0x80; i > 0; i >>= 1)
                            {
                                if (this.cm.GetBit() > 0)
                                {
                                    register1 |= i;
                                }

                                if (this.cm.GetBit() > 0)
                                {
                                    register2 |= i;
                                }
                            }

                            this.buffer.WriteByte(register1);
                            Logger.Debug("Write 2 : {0:X02}", register1);
                        }
                    }
                    while (--this.length > 0);
                    break;

                case 0xc0:
                    do
                    {
                        for (register1 = 0, i = 0x01; i > 0; i <<= 1)
                        {
                            if (this.cm.GetBit() > 0)
                            {
                                register1 |= i;
                            }
                        }

                        this.buffer.WriteByte(register1);
                        Logger.Debug("Write 3 : {0:X02}", register1);
                    }
                    while (--this.length > 0);
                    break;
            }
        }

        public void PrepareDecomp(BinaryReader first_byte, ushort out_len, MemoryStream out_buf)
        {
            this.bitplanesInfo = Convert.ToByte(first_byte.PeekByte() & 0xC0);
            this.length = out_len;
            this.buffer = out_buf;
        }
    }
}