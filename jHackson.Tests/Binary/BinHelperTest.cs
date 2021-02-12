// <copyright file="BinHelperTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Binary
{
    using System.Collections.Generic;
    using JHackson.Binary;
    using JHackson.Core;
    using NUnit.Framework;

    /// <summary>
    /// Provides a class to test the BinHelper class.
    /// </summary>
    public class BinHelperTest
    {
        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(uint, EnumEndianType)"/> method to get bytes in big endian from uint value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUintBigEndian()
        {
            uint value = 0x25523441;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.BigEndian))
            {
                result.Add(b);
            }

            Assert.That(0x25, Is.EqualTo(result[0]));
            Assert.That(0x52, Is.EqualTo(result[1]));
            Assert.That(0x34, Is.EqualTo(result[2]));
            Assert.That(0x41, Is.EqualTo(result[3]));
        }

        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(uint, EnumEndianType)"/> method to get bytes in little endian from uint value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUintLittleEndian()
        {
            uint value = 0x25523441;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.LittleEndian))
            {
                result.Add(b);
            }

            Assert.That(0x41, Is.EqualTo(result[0]));
            Assert.That(0x34, Is.EqualTo(result[1]));
            Assert.That(0x52, Is.EqualTo(result[2]));
            Assert.That(0x25, Is.EqualTo(result[3]));
        }

        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(ulong, EnumEndianType)"/> method to get bytes in big endian from ulong value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUlongBigEndian()
        {
            ulong value = 0x25523441ABDECF90;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.BigEndian))
            {
                result.Add(b);
            }

            Assert.That(0x25, Is.EqualTo(result[0]));
            Assert.That(0x52, Is.EqualTo(result[1]));
            Assert.That(0x34, Is.EqualTo(result[2]));
            Assert.That(0x41, Is.EqualTo(result[3]));
            Assert.That(0xAB, Is.EqualTo(result[4]));
            Assert.That(0xDE, Is.EqualTo(result[5]));
            Assert.That(0xCF, Is.EqualTo(result[6]));
            Assert.That(0x90, Is.EqualTo(result[7]));
        }

        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(ulong, EnumEndianType)"/> method to get bytes in little endian from ulong value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUlongLittleEndian()
        {
            ulong value = 0x25523441ABDECF90;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.LittleEndian))
            {
                result.Add(b);
            }

            Assert.That(0x90, Is.EqualTo(result[0]));
            Assert.That(0xCF, Is.EqualTo(result[1]));
            Assert.That(0xDE, Is.EqualTo(result[2]));
            Assert.That(0xAB, Is.EqualTo(result[3]));
            Assert.That(0x41, Is.EqualTo(result[4]));
            Assert.That(0x34, Is.EqualTo(result[5]));
            Assert.That(0x52, Is.EqualTo(result[6]));
            Assert.That(0x25, Is.EqualTo(result[7]));
        }

        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(ushort, EnumEndianType)"/> method to get bytes in big endian from ushort value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUshortBigEndian()
        {
            ushort value = 0x2552;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.BigEndian))
            {
                result.Add(b);
            }

            Assert.That(0x25, Is.EqualTo(result[0]));
            Assert.That(0x52, Is.EqualTo(result[1]));
        }

        /// <summary>
        /// Test the <see cref="BinHelper.GetBytes(ushort, EnumEndianType)"/> method to get bytes in little endian from ushort value.
        /// </summary>
        [Test]
        public void ShouldGiveBytesFromUshortLittleEndian()
        {
            ushort value = 0x2552;
            var result = new List<byte>();

            foreach (var b in BinHelper.GetBytes(value, EnumEndianType.LittleEndian))
            {
                result.Add(b);
            }

            Assert.That(0x52, Is.EqualTo(result[0]));
            Assert.That(0x25, Is.EqualTo(result[1]));
        }
    }
}