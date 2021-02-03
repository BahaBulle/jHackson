// <copyright file="TableElementNormalTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.TableElements
{
    using JHackson.Core.TableElements;
    using JHackson.Scripts.Tables.Elements;
    using NUnit.Framework;

    public class TableElementNormalTest
    {
        private ITableElement element;

        [SetUp]
        public void Setup()
        {
            this.element = new TableElementNormal()
                .WithLine("20= ");

            this.element.Init();
        }

        [Test]
        public void ShouldGetKeyBytes()
        {
            Assert.That(this.element.GetKeyBytes(), Is.EqualTo(new byte[] { 0x20 }));
        }

        [Test]
        public void ShouldGetKeySize()
        {
            Assert.That(this.element.KeySize, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetKeyString()
        {
            Assert.That(this.element.Key, Is.EqualTo("20"));
        }

        [Test]
        public void ShouldGetRegexValue()
        {
            Assert.That(this.element.RegexValue, Is.EqualTo(" "));
        }

        [Test]
        public void ShouldGetValueChars()
        {
            Assert.That(this.element.GetValueChars(), Is.EqualTo(new char[] { ' ' }));
        }

        [Test]
        public void ShouldGetValueSize()
        {
            Assert.That(this.element.ValueSize, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetValueString()
        {
            Assert.That(this.element.Value, Is.EqualTo(" "));
        }
    }
}