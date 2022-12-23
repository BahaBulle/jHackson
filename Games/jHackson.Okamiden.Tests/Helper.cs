// <copyright file="Helper.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.Tests
{
    using System.IO;
    using jHackson.Okamiden.EVB;

    internal static class Helper
    {
        public static EvbHeader GetHeader()
        {
            byte[] bytes = new byte[] { 0x1B, 0x4C, 0x75, 0x61, 0x51, 0x00, 0x01, 0x04, 0x04, 0x04, 0x04, 0x00 };

            var stream = Helper.GetStream(bytes);

            EvbHeader header;
            using (var reader = new BinaryReader(stream))
            {
                header = new EvbHeader(reader);
            }

            return header;
        }

        public static MemoryStream GetStream(byte[] bytes)
        {
            var stream = new MemoryStream();

            foreach (byte b in bytes)
            {
                stream.WriteByte(b);
            }

            stream.Position = 0;

            return stream;
        }
    }
}
