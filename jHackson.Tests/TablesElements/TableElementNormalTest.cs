using JHackson.Core.TableElements;
using JHackson.Tables.TableElements;
using NUnit.Framework;

namespace JHackson.Tests.TableElements
{
    public class TableElementNormalTest
    {
        private ITableElement element;

        [SetUp]
        public void Setup()
        {
            element = new TableElementNormal()
                .WithLine("20= ");

            element.Init();
        }

        [Test]
        public void ShouldGetKeyBytes()
        {
            Assert.That(element.KeyBytes, Is.EqualTo(new byte[] { 0x20 }));
        }

        [Test]
        public void ShouldGetKeySize()
        {
            Assert.That(element.KeySize, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetKeyString()
        {
            Assert.That(element.Key, Is.EqualTo("20"));
        }

        [Test]
        public void ShouldGetRegexValue()
        {
            Assert.That(element.RegexValue, Is.EqualTo(" "));
        }

        [Test]
        public void ShouldGetValueChars()
        {
            Assert.That(element.ValueChars, Is.EqualTo(new char[] { ' ' }));
        }

        [Test]
        public void ShouldGetValueSize()
        {
            Assert.That(element.ValueSize, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetValueString()
        {
            Assert.That(element.Value, Is.EqualTo(" "));
        }
    }
}