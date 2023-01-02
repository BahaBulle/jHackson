// <copyright file="UTEvbFile.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden.Tests
{
    using System.IO;
    using jHackson.Okamiden.EVB;
    using NUnit.Framework;

    public class UTEvbFile
    {
        [Test]
        public void ShouldParseEvbConstantsCollection()
        {
            byte[] bytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x04, 0x06, 0x00, 0x00, 0x00, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x00 };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var constants = new EvbConstantsCollection(reader, header);

                Assert.That(constants, Has.Count.EqualTo(1));
                Assert.That(constants[0].String, Is.EqualTo("Hello"));
            }
        }

        [Test]
        public void ShouldParseEvbFunctionsCollection()
        {
            byte[] bytes = {
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x03, 0x09, 0x0D, 0x00, 0x00, 0x00, 0x45, 0x00, 0x00, 0x00, 0x8A, 0x00, 0x00, 0x00,
                0xE5, 0x00, 0x00, 0x00, 0xA2, 0x40, 0x00, 0x00, 0x5C, 0x00, 0x01, 0x01, 0x16, 0xC0, 0x00, 0x80,
                0x85, 0x41, 0x00, 0x00, 0xC0, 0x01, 0x00, 0x02, 0x00, 0x02, 0x80, 0x02, 0x9C, 0x41, 0x80, 0x01,
                0x61, 0x80, 0x00, 0x00, 0x16, 0x40, 0xFE, 0x7F, 0x1E, 0x00, 0x80, 0x00, 0x02, 0x00, 0x00, 0x00,
                0x04, 0x07, 0x00, 0x00, 0x00, 0x69, 0x70, 0x61, 0x69, 0x72, 0x73, 0x00, 0x04, 0x06, 0x00, 0x00,
                0x00, 0x70, 0x72, 0x69, 0x6E, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D, 0x00, 0x00, 0x00, 0x02,
                0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02,
                0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x03,
                0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x05,
                0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x61, 0x72, 0x67, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x28, 0x66, 0x6F, 0x72, 0x20,
                0x67, 0x65, 0x6E, 0x65, 0x72, 0x61, 0x74, 0x6F, 0x72, 0x29, 0x00, 0x05, 0x00, 0x00, 0x00, 0x0C,
                0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x28, 0x66, 0x6F, 0x72, 0x20, 0x73, 0x74, 0x61, 0x74,
                0x65, 0x29, 0x00, 0x05, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x28,
                0x66, 0x6F, 0x72, 0x20, 0x63, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x29, 0x00, 0x05, 0x00, 0x00,
                0x00, 0x0C, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x69, 0x00, 0x06, 0x00, 0x00, 0x00, 0x0A,
                0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x76, 0x00, 0x06, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00
            };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var function = new EvbFunctionsCollection(reader, header);

                Assert.That(function, Has.Count.EqualTo(1));
                Assert.That(function[0].LineStart, Is.EqualTo(1));
                Assert.That(function[0].LineEnd, Is.EqualTo(5));
                Assert.That(function[0].NumberOfUpValues, Is.EqualTo(0));
                Assert.That(function[0].NumberOfParams, Is.EqualTo(0));
                Assert.That(function[0].MaxStack, Is.EqualTo(9));
                Assert.That(function[0].Instructions.Count, Is.EqualTo(13));
                Assert.That(function[0].Constants.Count, Is.EqualTo(2));
                Assert.That(function[0].Functions.Count, Is.EqualTo(0));
                Assert.That(function[0].LinesPositions.Count, Is.EqualTo(13));
                Assert.That(function[0].Locals.Count, Is.EqualTo(6));
                Assert.That(function[0].UpValues.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public void ShouldParseEvbHeader()
        {
            byte[] bytes = new byte[] { 0x1B, 0x4C, 0x75, 0x61, 0x51, 0x00, 0x01, 0x04, 0x04, 0x04, 0x04, 0x00 };

            var stream = Helper.GetStream(bytes);

            using (var reader = new BinaryReader(stream))
            {
                var header = new EvbHeader(reader);

                Assert.That(header.Id[0], Is.EqualTo(bytes[0]));
                Assert.That(header.Id[1], Is.EqualTo(bytes[1]));
                Assert.That(header.Id[2], Is.EqualTo(bytes[2]));
                Assert.That(header.Id[3], Is.EqualTo(bytes[3]));
                Assert.That(header.Version, Is.EqualTo(bytes[4]));
                Assert.That(header.Format, Is.EqualTo(bytes[5]));
                Assert.That(header.IsLittleEndian, Is.EqualTo(true));
                Assert.That(header.SizeOfInt, Is.EqualTo(bytes[7]));
                Assert.That(header.SizeOfSizeT, Is.EqualTo(bytes[8]));
                Assert.That(header.SizeOfInstruction, Is.EqualTo(bytes[9]));
                Assert.That(header.SizeOfLuaNumber, Is.EqualTo(bytes[10]));
                Assert.That(header.IsIntegral, Is.EqualTo(false));
            }
        }

        [Test]
        public void ShouldParseEvbInstructionsCollection()
        {
            byte[] bytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x0A, 0xC0, 0x01, 0x00 };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var instructions = new EvbInstructionsCollection(reader, header);

                Assert.That(instructions, Has.Count.EqualTo(1));
                Assert.That(instructions[0], Is.EqualTo(0x01C00A));
            }
        }

        [Test]
        public void ShouldParseEvbLinesPositionsCollection()
        {
            byte[] bytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x44, 0x33, 0x22, 0x11 };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var linesPositions = new EvbLinesPositionsCollection(reader, header);

                Assert.That(linesPositions, Has.Count.EqualTo(1));
                Assert.That(linesPositions[0], Is.EqualTo(0x11223344));
            }
        }

        [Test]
        public void ShouldParseEvbLocalsCollection()
        {
            byte[] bytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x00, 0x01, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00 };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var constants = new EvbLocalsCollection(reader, header);

                Assert.That(constants, Has.Count.EqualTo(1));
                Assert.That(constants[0].Name, Is.EqualTo("Hello"));
                Assert.That(constants[0].Start, Is.EqualTo(0x01));
                Assert.That(constants[0].End, Is.EqualTo(0x0C));
            }
        }

        [Test]
        public void ShouldParseEvbUpValuesCollection()
        {
            byte[] bytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x00 };

            var stream = Helper.GetStream(bytes);
            var header = Helper.GetHeader();

            using (var reader = new BinaryReader(stream))
            {
                var upValues = new EvbUpValuesCollection(reader, header);

                Assert.That(upValues, Has.Count.EqualTo(1));
                Assert.That(upValues[0], Is.EqualTo("Hello"));
            }
        }
    }
}
