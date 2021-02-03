// <copyright file="TableElementWithParamTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.TableElements
{
    using System.Collections.Generic;
    using JHackson.Core.TableElements;
    using JHackson.Scripts.Tables.Elements;
    using NUnit.Framework;

    public class TableElementWithParamTest
    {
        private const string IDENTIFIER = "%";

        private const string KEY = "0102";

        private const string VALUE = "<Pause value='%1' next='%1'>";

        private ITableElement element;

        [SetUp]
        public void Setup()
        {
            this.element = new TableElementWithParam()
                .WithLine($"{IDENTIFIER}{KEY}={VALUE}");

            this.element.Init();
        }

        [Test]
        public void ShouldGetKeyBytes()
        {
            Assert.That(this.element.GetKeyBytes(), Is.EqualTo(new byte[] { 0x01, 0x02 }));
        }

        [Test]
        public void ShouldGetKeySize()
        {
            Assert.That(this.element.KeySize, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetKeyString()
        {
            Assert.That(this.element.Key, Is.EqualTo(KEY));
        }

        [Test]
        public void ShouldGetListParam()
        {
            var result = new List<ITableElementParam>()
            {
                new TableElementParam()
                {
                    NbBytes = 1,
                    Position = 14,
                    Value = "%1",
                },
                new TableElementParam()
                {
                    NbBytes = 1,
                    Position = 24,
                    Value = "%1",
                },
            };

            CollectionAssert.AreEqual(result, this.element.ListParam);
        }

        [Test]
        public void ShouldGetNbParam()
        {
            Assert.That(this.element.NbParam, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetRegexValue()
        {
            Assert.That(this.element.RegexValue, Is.EqualTo("<Pause value='[0-9A-Fa-f]{2}' next='[0-9A-Fa-f]{2}'>"));
        }

        [Test]
        public void ShouldGetValueChars()
        {
            Assert.That(this.element.GetValueChars(), Is.EqualTo(VALUE.ToCharArray()));
        }

        [Test]
        public void ShouldGetValueSize()
        {
            Assert.That(this.element.ValueSize, Is.EqualTo(VALUE.Length - (1 + 1 + 1 + 1) + (1 * 2) + (1 * 2)));
        }

        [Test]
        public void ShouldGetValueString()
        {
            Assert.That(this.element.Value, Is.EqualTo(VALUE));
        }
    }
}