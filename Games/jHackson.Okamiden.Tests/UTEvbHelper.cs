// <copyright file="UTEvbHelper.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.Tests
{
    using System.IO;
    using jHackson.Okamiden.EVB;
    using NUnit.Framework;

    public class UTEvbHelper
    {
        [Test]
        public void ShouldReadIntegerBigEndian1()
        {
            byte[] bytes = new byte[] { 0x80 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, false, 1);

                Assert.That(result, Is.EqualTo(0x80));
            }
        }

        [Test]
        public void ShouldReadIntegerBigEndian2()
        {
            byte[] bytes = new byte[] { 0x70, 0x80 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, false, 2);

                Assert.That(result, Is.EqualTo(0x7080));
            }
        }

        [Test]
        public void ShouldReadIntegerBigEndian4()
        {
            byte[] bytes = new byte[] { 0x50, 0x60, 0x70, 0x80 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, false, 4);

                Assert.That(result, Is.EqualTo(0x50607080));
            }
        }

        [Test]
        public void ShouldReadIntegerBigEndian8()
        {
            byte[] bytes = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, false, 8);

                Assert.That(result, Is.EqualTo(0x1020304050607080));
            }
        }

        [Test]
        public void ShouldReadIntegerLittleEndian1()
        {
            byte[] bytes = new byte[] { 0x80 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, true, 1);

                Assert.That(result, Is.EqualTo(0x80));
            }
        }

        [Test]
        public void ShouldReadIntegerLittleEndian2()
        {
            byte[] bytes = new byte[] { 0x80, 0x70 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, true, 2);

                Assert.That(result, Is.EqualTo(0x7080));
            }
        }

        [Test]
        public void ShouldReadIntegerLittleEndian4()
        {
            byte[] bytes = new byte[] { 0x80, 0x70, 0x60, 0x50 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, true, 4);

                Assert.That(result, Is.EqualTo(0x50607080));
            }
        }

        [Test]
        public void ShouldReadIntegerLittleEndian8()
        {
            byte[] bytes = new byte[] { 0x80, 0x70, 0x60, 0x50, 0x40, 0x30, 0x20, 0x10 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                ulong result = EvbHelper.ReadInteger(reader, true, 8);

                Assert.That(result, Is.EqualTo(0x1020304050607080));
            }
        }

        [Test]
        public void ShouldReadNumberDouble()
        {
            byte[] bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x40 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                double result = EvbHelper.ReadNumber(reader, 8);

                Assert.That(result, Is.EqualTo(2.5));
            }
        }

        [Test]
        public void ShouldReadNumberFloat()
        {
            byte[] bytes = new byte[] { 0x00, 0x00, 0x20, 0x40 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                double result = EvbHelper.ReadNumber(reader, 4);

                Assert.That(result, Is.EqualTo(2.5));
            }
        }

        [Test]
        public void ShouldReadString()
        {
            byte[] bytes = new byte[] { 0x06, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x00 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                string? result = EvbHelper.ReadString(reader, true, 1);

                Assert.That(result, Is.EqualTo("Hello"));
            }

        }
    }
}