using JHackson.Core.TableElements;
using JHackson.Tables.TableElements;
using NUnit.Framework;
using System.Collections.Generic;

namespace JHackson.Tests.TableElements
{
    public class TableElementWithParamTest
    {
        private const string IDENTIFIER = "%";
        private const string KEY = "0102";
        private const string VALUE = "<Pause value='%1' next='%1'>";

        private ITableElement element;

        [SetUp]
        public void Setup()
        {
            element = new TableElementWithParam()
                .WithLine($"{IDENTIFIER}{KEY}={VALUE}");

            element.Init();
        }

        [Test]
        public void ShouldGetKeyBytes()
        {
            Assert.That(element.KeyBytes, Is.EqualTo(new byte[] { 0x01, 0x02 }));
        }

        [Test]
        public void ShouldGetKeySize()
        {
            Assert.That(element.KeySize, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetKeyString()
        {
            Assert.That(element.Key, Is.EqualTo(KEY));
        }

        [Test]
        public void ShouldGetListParam()
        {
            List<ITableElementParam> result = new List<ITableElementParam>()
            {
                new TableElementParam()
                {
                    NbBytes = 1,
                    Position = 14,
                    Value = "%1"
                },
                new TableElementParam()
                {
                    NbBytes = 1,
                    Position = 24,
                    Value = "%1"
                },
            };

            CollectionAssert.AreEqual(result, element.ListParam);
        }

        [Test]
        public void ShouldGetNbParam()
        {
            Assert.That(element.NbParam, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetRegexValue()
        {
            Assert.That(element.RegexValue, Is.EqualTo("<Pause value='[0-9A-Fa-f]{2}' next='[0-9A-Fa-f]{2}'>"));
        }

        [Test]
        public void ShouldGetValueChars()
        {
            Assert.That(element.ValueChars, Is.EqualTo(VALUE.ToCharArray()));
        }

        [Test]
        public void ShouldGetValueSize()
        {
            Assert.That(element.ValueSize, Is.EqualTo(VALUE.Length - (1 + 1 + 1 + 1) + (1 * 2 + 1 * 2)));
        }

        [Test]
        public void ShouldGetValueString()
        {
            Assert.That(element.Value, Is.EqualTo(VALUE));
        }
    }
}