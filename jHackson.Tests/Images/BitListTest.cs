// <copyright file="BitListTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Images
{
    using System.Collections.Generic;
    using JHackson.Image;
    using NUnit.Framework;

    internal class BitListTest
    {
        private readonly byte[] hexData = new byte[] { 0x38, 0x30 };

        /// <summary>
        /// Test to add a bit in the array.
        /// </summary>
        [Test]
        public void ShouldAddBit()
        {
            var bits = new BitList(this.hexData);

            bits.Add(true);

            Assert.That(bits.Count, Is.EqualTo(17));
        }

        /// <summary>
        /// Test to add a BitList in the current BitList.
        /// </summary>
        [Test]
        public void ShouldAddBitList()
        {
            var bits = new BitList(this.hexData);

            var bitsToAdd = new BitList(new byte[] { 0x28, 0x20 });

            bits.AddBitList(bitsToAdd);

            Assert.That(bits.Count, Is.EqualTo(32));
        }

        /// <summary>
        /// Test to add multiple bit in the array.
        /// </summary>
        [Test]
        public void ShouldAddRangeBits()
        {
            var bits = new BitList(this.hexData);

            bits.AddRange(new List<bool>() { true, true, false });

            Assert.That(bits.Count, Is.EqualTo(19));
        }

        /// <summary>
        /// Test to concat two BitList in a new one.
        /// </summary>
        [Test]
        public void ShouldConcatTwoBitList()
        {
            var bits1 = new BitList(this.hexData);
            var bits2 = new BitList(this.hexData);

            var bitsConcated = bits1.Concat(bits2);

            Assert.That(bitsConcated.Count, Is.EqualTo(32));
        }

        /// <summary>
        /// Test to convert a BitList into bytes array.
        /// </summary>
        [Test]
        public void ShouldConvertBitsIntoBytes()
        {
            var bits = new BitList(this.hexData);

            var bytes = bits.ToBytes();

            Assert.That(this.hexData, Is.EqualTo(bytes));
        }

        /// <summary>
        /// Test that Add method don't add a bit in a fixed size BitList.
        /// </summary>
        [Test]
        public void ShouldNotAddBitAtEnd()
        {
            var bits = new BitList(4, false);

            bits.Add(true);

            Assert.That(bits.Count, Is.EqualTo(4));
        }

        /// <summary>
        /// Test to perform a bitwise AND operation.
        /// </summary>
        [Test]
        public void ShouldPerformAndOperation()
        {
            var bits = new BitList(this.hexData);

            var bitsAnd = new BitList(new byte[] { 0x40, 0x48 });

            bits.And(bitsAnd);

            foreach (var value in bits)
            {
                Assert.That(value, Is.EqualTo(false));
            }
        }

        /// <summary>
        /// Test to perform a bitwise Lsl operation.
        /// </summary>
        [Test]
        public void ShouldPerformLslOperation()
        {
            var bits = new BitList(this.hexData);

            bits.Lsl(3);

            Assert.That(bits[0], Is.EqualTo(true));
            Assert.That(bits[1], Is.EqualTo(true));
            Assert.That(bits[2], Is.EqualTo(false));
            Assert.That(bits[3], Is.EqualTo(false));
            Assert.That(bits[4], Is.EqualTo(false));
            Assert.That(bits[5], Is.EqualTo(false));
            Assert.That(bits[6], Is.EqualTo(false));
            Assert.That(bits[7], Is.EqualTo(true));
            Assert.That(bits[8], Is.EqualTo(true));
            Assert.That(bits[9], Is.EqualTo(false));
            Assert.That(bits[10], Is.EqualTo(false));
            Assert.That(bits[11], Is.EqualTo(false));
            Assert.That(bits[12], Is.EqualTo(false));
            Assert.That(bits[13], Is.EqualTo(false));
            Assert.That(bits[14], Is.EqualTo(false));
            Assert.That(bits[15], Is.EqualTo(false));
        }

        /// <summary>
        /// Test to perform a bitwise NOT operation.
        /// </summary>
        [Test]
        public void ShouldPerformNotOperation()
        {
            var bits = new BitList(this.hexData);

            bits.Not();

            Assert.That(bits[0], Is.EqualTo(true));
            Assert.That(bits[1], Is.EqualTo(true));
            Assert.That(bits[2], Is.EqualTo(false));
            Assert.That(bits[3], Is.EqualTo(false));
            Assert.That(bits[4], Is.EqualTo(false));
            Assert.That(bits[5], Is.EqualTo(true));
            Assert.That(bits[6], Is.EqualTo(true));
            Assert.That(bits[7], Is.EqualTo(true));
            Assert.That(bits[8], Is.EqualTo(true));
            Assert.That(bits[9], Is.EqualTo(true));
            Assert.That(bits[10], Is.EqualTo(false));
            Assert.That(bits[11], Is.EqualTo(false));
            Assert.That(bits[12], Is.EqualTo(true));
            Assert.That(bits[13], Is.EqualTo(true));
            Assert.That(bits[14], Is.EqualTo(true));
            Assert.That(bits[15], Is.EqualTo(true));
        }

        /// <summary>
        /// Test to perform a bitwise OR operation.
        /// </summary>
        [Test]
        public void ShouldPerformOrOperation()
        {
            var bits = new BitList(this.hexData);

            var bitsOr = new BitList(new byte[] { 0x40, 0x48 });

            bits.Or(bitsOr);

            Assert.That(bits[0], Is.EqualTo(false));
            Assert.That(bits[1], Is.EqualTo(true));
            Assert.That(bits[2], Is.EqualTo(true));
            Assert.That(bits[3], Is.EqualTo(true));
            Assert.That(bits[4], Is.EqualTo(true));
            Assert.That(bits[5], Is.EqualTo(false));
            Assert.That(bits[6], Is.EqualTo(false));
            Assert.That(bits[7], Is.EqualTo(false));
            Assert.That(bits[8], Is.EqualTo(false));
            Assert.That(bits[9], Is.EqualTo(true));
            Assert.That(bits[10], Is.EqualTo(true));
            Assert.That(bits[11], Is.EqualTo(true));
            Assert.That(bits[12], Is.EqualTo(true));
            Assert.That(bits[13], Is.EqualTo(false));
            Assert.That(bits[14], Is.EqualTo(false));
            Assert.That(bits[15], Is.EqualTo(false));
        }

        /// <summary>
        /// Test to perform a bitwise ROL operation.
        /// </summary>
        [Test]
        public void ShouldPerformRolOperation()
        {
            var bits = new BitList(this.hexData);

            bits.Rol(3);

            Assert.That(bits[0], Is.EqualTo(true));
            Assert.That(bits[1], Is.EqualTo(true));
            Assert.That(bits[2], Is.EqualTo(false));
            Assert.That(bits[3], Is.EqualTo(false));
            Assert.That(bits[4], Is.EqualTo(false));
            Assert.That(bits[5], Is.EqualTo(false));
            Assert.That(bits[6], Is.EqualTo(false));
            Assert.That(bits[7], Is.EqualTo(true));
            Assert.That(bits[8], Is.EqualTo(true));
            Assert.That(bits[9], Is.EqualTo(false));
            Assert.That(bits[10], Is.EqualTo(false));
            Assert.That(bits[11], Is.EqualTo(false));
            Assert.That(bits[12], Is.EqualTo(false));
            Assert.That(bits[13], Is.EqualTo(false));
            Assert.That(bits[14], Is.EqualTo(false));
            Assert.That(bits[15], Is.EqualTo(true));
        }

        /// <summary>
        /// Test to perform a bitwise ROR operation.
        /// </summary>
        [Test]
        public void ShouldPerformRorOperation()
        {
            var bits = new BitList(this.hexData);

            bits.Ror(3);

            Assert.That(bits[0], Is.EqualTo(false));
            Assert.That(bits[1], Is.EqualTo(false));
            Assert.That(bits[2], Is.EqualTo(false));
            Assert.That(bits[3], Is.EqualTo(false));
            Assert.That(bits[4], Is.EqualTo(false));
            Assert.That(bits[5], Is.EqualTo(true));
            Assert.That(bits[6], Is.EqualTo(true));
            Assert.That(bits[7], Is.EqualTo(true));
            Assert.That(bits[8], Is.EqualTo(false));
            Assert.That(bits[9], Is.EqualTo(false));
            Assert.That(bits[10], Is.EqualTo(false));
            Assert.That(bits[11], Is.EqualTo(false));
            Assert.That(bits[12], Is.EqualTo(false));
            Assert.That(bits[13], Is.EqualTo(true));
            Assert.That(bits[14], Is.EqualTo(true));
            Assert.That(bits[15], Is.EqualTo(false));
        }

        /// <summary>
        /// Test to perform a bitwise Rsl operation.
        /// </summary>
        [Test]
        public void ShouldPerformRslOperation()
        {
            var bits = new BitList(this.hexData);

            bits.Rsl(3);

            Assert.That(bits[0], Is.EqualTo(false));
            Assert.That(bits[1], Is.EqualTo(false));
            Assert.That(bits[2], Is.EqualTo(false));
            Assert.That(bits[3], Is.EqualTo(false));
            Assert.That(bits[4], Is.EqualTo(false));
            Assert.That(bits[5], Is.EqualTo(true));
            Assert.That(bits[6], Is.EqualTo(true));
            Assert.That(bits[7], Is.EqualTo(true));
            Assert.That(bits[8], Is.EqualTo(false));
            Assert.That(bits[9], Is.EqualTo(false));
            Assert.That(bits[10], Is.EqualTo(false));
            Assert.That(bits[11], Is.EqualTo(false));
            Assert.That(bits[12], Is.EqualTo(false));
            Assert.That(bits[13], Is.EqualTo(true));
            Assert.That(bits[14], Is.EqualTo(true));
            Assert.That(bits[15], Is.EqualTo(false));
        }

        /// <summary>
        /// Test to perform a bitwise XOR operation.
        /// </summary>
        [Test]
        public void ShouldPerformXorOperation()
        {
            var bits = new BitList(this.hexData);

            var bitsXor = new BitList(new byte[] { 0xC7, 0xCF });

            bits.Xor(bitsXor);

            foreach (var value in bits)
            {
                Assert.That(value, Is.EqualTo(true));
            }
        }

        /// <summary>
        /// Test rearragment of bits of a BitList.
        /// </summary>
        [Test]
        public void ShouldRearrangeBits()
        {
            var data = new byte[] { 0x18, 0x10 };

            var plan = new List<List<short>>()
            {
                new List<short>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0,
                },
                new List<short>()
                {
                    1, 1, 1, 1, 1, 1, 1, 1,
                },
            };
            var pattern = new TilePattern(plan)
            {
                Interleave = false,
                Size = 2,
            };

            var bits = new BitList(data);

            var bitsRearranged = bits.RearrangeBitsWith2Planes(pattern);

            Assert.That(bitsRearranged[0], Is.EqualTo(false));
            Assert.That(bitsRearranged[1], Is.EqualTo(false));
            Assert.That(bitsRearranged[2], Is.EqualTo(false));
            Assert.That(bitsRearranged[3], Is.EqualTo(false));
            Assert.That(bitsRearranged[4], Is.EqualTo(false));
            Assert.That(bitsRearranged[5], Is.EqualTo(false));
            Assert.That(bitsRearranged[6], Is.EqualTo(true));
            Assert.That(bitsRearranged[7], Is.EqualTo(true));
            Assert.That(bitsRearranged[8], Is.EqualTo(true));
            Assert.That(bitsRearranged[9], Is.EqualTo(false));
            Assert.That(bitsRearranged[10], Is.EqualTo(false));
            Assert.That(bitsRearranged[11], Is.EqualTo(false));
            Assert.That(bitsRearranged[12], Is.EqualTo(false));
            Assert.That(bitsRearranged[13], Is.EqualTo(false));
            Assert.That(bitsRearranged[14], Is.EqualTo(false));
            Assert.That(bitsRearranged[15], Is.EqualTo(false));
        }

        /// <summary>
        /// Test to set a bit to true.
        /// </summary>
        [Test]
        public void ShouldSetABitToTrue()
        {
            var bits = new BitList(this.hexData);

            bits.Set(6, true);

            Assert.That(bits[6], Is.EqualTo(true));
        }

        /// <summary>
        /// Test to set all bits to true.
        /// </summary>
        [Test]
        public void ShouldSetAllBitToTrue()
        {
            var bits = new BitList(this.hexData);

            bits.SetAll(true);

            foreach (var value in bits)
            {
                Assert.That(value, Is.EqualTo(true));
            }
        }
    }
}