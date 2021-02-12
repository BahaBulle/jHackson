// <copyright file="SerializeTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Json
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Binary;
    using JHackson.Binary.Actions;
    using JHackson.Core;
    using JHackson.Core.Common;
    using JHackson.Image;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SkiaSharp;

    /// <summary>
    /// Provides a class to test serialization and deserialization.
    /// </summary>
    public class SerializeTest
    {
        /// <summary>
        /// Test the serialization of the action BinCopy.
        /// </summary>
        [Test]
        public void ShouldSerializeActionBinCopy()
        {
            var expected = "{" +
                "\"Destination\":{" +
                    "\"AdressEnd\":768," +
                    "\"AdressStart\":256," +
                    "\"Size\":512}," +
                "\"From\":1," +
                "\"Source\":{" +
                    "\"AdressEnd\":256," +
                    "\"AdressStart\":0," +
                    "\"Size\":256}," +
                "\"To\":2," +
                "\"Title\":\"Test ActionBinCopy\"," +
                "\"Todo\":true}";

            var action = new ActionBinCopy()
            {
                Destination = new BufferParameters() { AdressStart = 0x100, AdressEnd = 0x300, Size = 0x200 },
                From = 1,
                Source = new BufferParameters() { AdressStart = 0, AdressEnd = 0x100, Size = 0x100 },
                Title = "Test ActionBinCopy",
                To = 2,
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of the action BinLoad.
        /// </summary>
        [Test]
        public void ShouldSerializeActionBinLoad()
        {
            var expected = "{" +
                "\"FileName\":\"fichier.txt\"," +
                "\"To\":1," +
                "\"Title\":\"Test ActionBinLoad\"," +
                "\"Todo\":true}";

            var action = new ActionBinLoad()
            {
                FileName = "fichier.txt",
                Title = "Test ActionBinLoad",
                To = 1,
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of the action BinModify.
        /// </summary>
        [Test]
        public void ShouldSerializeActionBinModify()
        {
            var expected = "{" +
                "\"DataParameters\":[{\"Adress\":{\"Origin\":\"Begin\",\"Value\":\"0x64\"},\"Endian\":\"BigEndian\",\"Type\":\"U16\",\"Value\":0}]," +
                "\"To\":1," +
                "\"Title\":\"Test ActionBinModify\"," +
                "\"Todo\":true}";

            var action = new ActionBinModify()
            {
                Title = "Test ActionBinModify",
                To = 1,
                Todo = true,
            };

            action.DataParameters.Add(new DataParameters() { Adress = new Adress() { Value = 100, Origin = SeekOrigin.Begin }, Endian = EnumEndianType.BigEndian, Type = EnumDataType.U16, Value = 0, });

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of the action Save in bin mode.
        /// </summary>
        [Test]
        public void ShouldSerializeActionBinSave()
        {
            var expected = "{" +
                "\"FileName\":\"fichier.txt\"," +
                "\"Format\":\"Bin\"," +
                "\"From\":1," +
                "\"Source\":{\"AdressEnd\":null,\"AdressStart\":null,\"Size\":null}," +
                "\"Title\":\"Test ActionBinSave\"," +
                "\"Todo\":true}";

            var action = new ActionBinSave()
            {
                FileName = "fichier.txt",
                Format = "Bin",
                From = 1,
                Title = "Test ActionBinSave",
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of a DataParameters.
        /// </summary>
        [Test]
        public void ShouldSerializeDataParameters()
        {
            var expected = "{" +
                "\"Adress\":{\"Origin\":\"Begin\",\"Value\":\"0x64\"}," +
                "\"Endian\":\"BigEndian\"," +
                "\"Type\":\"U16\"," +
                "\"Value\":0}";

            var parameters = new DataParameters()
            {
                Adress = new Adress()
                {
                    Origin = SeekOrigin.Begin,
                    Value = 100,
                },
                Endian = EnumEndianType.BigEndian,
                Type = EnumDataType.U16,
                Value = 0,
            };

            var result = JsonConvert.SerializeObject(parameters);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of an ImagePattern.
        /// </summary>
        [Test]
        public void ShouldSerializeImagePattern()
        {
            var expected = "{" +
                "\"Format\":\"2BPP GB\"," +
                "\"Height\":96," +
                "\"Palette\":[{\"Alpha\":255,\"Red\":0,\"Green\":0,\"Blue\":0,\"Hue\":0.0},{\"Alpha\":255,\"Red\":8,\"Green\":82,\"Blue\":66,\"Hue\":167.02704},{\"Alpha\":255,\"Red\":90,\"Green\":140,\"Blue\":255,\"Hue\":221.8182},{\"Alpha\":255,\"Red\":239,\"Green\":239,\"Blue\":239,\"Hue\":0.0}]," +
                "\"TilePattern\":null," +
                "\"Width\":128}";

            var parameters = new ImagePattern()
            {
                Format = "2BPP GB",
                Height = 96,
                Width = 128,
            };
            parameters.Palette.Add(new SKColor(0x00, 0x00, 0x00, 0xFF));
            parameters.Palette.Add(new SKColor(0x08, 0x52, 0x42, 0xFF));
            parameters.Palette.Add(new SKColor(0x5A, 0x8C, 0xFF, 0xFF));
            parameters.Palette.Add(new SKColor(0xEF, 0xEF, 0xEF, 0xFF));

            var result = JsonConvert.SerializeObject(parameters);

            Assert.That(expected, Is.EqualTo(result));
        }

        /// <summary>
        /// Test the serialization of a TilePattern.
        /// </summary>
        [Test]
        public void ShouldSerializeTilePattern()
        {
            var expected = "{" +
                "\"Height\":8," +
                "\"Interleave\":true," +
                "\"Map\":[[0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,4,4,4,4,4,4,4,4,6,6,6,6,6,6,6,6,8,8,8,8,8,8,8,8,10,10,10,10,10,10,10,10,12,12,12,12,12,12,12,12,14,14,14,14,14,14,14,14],[1,1,1,1,1,1,1,1,3,3,3,3,3,3,3,3,5,5,5,5,5,5,5,5,7,7,7,7,7,7,7,7,9,9,9,9,9,9,9,9,11,11,11,11,11,11,11,11,13,13,13,13,13,13,13,13,15,15,15,15,15,15,15,15]]," +
                "\"Order\":\"Planar\"," +
                "\"Size\":16," +
                "\"Width\":8}";

            var tilePattern = new TilePattern()
            {
                Height = 8,
                Interleave = true,
                Order = EnumTileOrder.Planar,
                Size = 16,
                Width = 8,
            };
            tilePattern.Map.AddRange(new List<List<short>>()
            {
                new List<short>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0,
                    2, 2, 2, 2, 2, 2, 2, 2,
                    4, 4, 4, 4, 4, 4, 4, 4,
                    6, 6, 6, 6, 6, 6, 6, 6,
                    8, 8, 8, 8, 8, 8, 8, 8,
                    10, 10, 10, 10, 10, 10, 10, 10,
                    12, 12, 12, 12, 12, 12, 12, 12,
                    14, 14, 14, 14, 14, 14, 14, 14,
                },
                new List<short>()
                {
                    1, 1, 1, 1, 1, 1, 1, 1,
                    3, 3, 3, 3, 3, 3, 3, 3,
                    5, 5, 5, 5, 5, 5, 5, 5,
                    7, 7, 7, 7, 7, 7, 7, 7,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    11, 11, 11, 11, 11, 11, 11, 11,
                    13, 13, 13, 13, 13, 13, 13, 13,
                    15, 15, 15, 15, 15, 15, 15, 15,
                },
            });

            var result = JsonConvert.SerializeObject(tilePattern);

            Assert.That(expected, Is.EqualTo(result));
        }
    }
}