// <copyright file="DataParametersTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Binary
{
    using System.IO;
    using JHackson.Binary;
    using NUnit.Framework;

    public class DataParametersTest
    {
        [Test]
        public void ShouldConvertAdressInHex()
        {
            var data = new DataParameters("0x100");

            data.CheckAdress();

            Assert.That(data.Adress, Is.EqualTo(0x100));
            Assert.That(data.Origin, Is.EqualTo(SeekOrigin.Begin));
        }

        [Test]
        public void ShouldConvertAdressInNumber()
        {
            var data = new DataParameters("100");

            data.CheckAdress();

            Assert.That(data.Adress, Is.EqualTo(100));
            Assert.That(data.Origin, Is.EqualTo(SeekOrigin.Begin));
        }

        [Test]
        public void ShouldConvertAdressInString()
        {
            var data = new DataParameters("SEEK_END");

            data.CheckAdress();

            Assert.That(data.Adress, Is.EqualTo(0));
            Assert.That(data.Origin, Is.EqualTo(SeekOrigin.End));
        }
    }
}