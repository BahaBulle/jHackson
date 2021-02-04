// <copyright file="DeserializeTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Json
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Binary;
    using JHackson.Binary.Actions;
    using JHackson.Image;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SkiaSharp;

    /// <summary>
    /// Provides a class to test serialization and deserialization.
    /// </summary>
    public class DeserializeTest
    {
        /// <summary>
        /// Test the deserialization of the action BinModify.
        /// </summary>
        [Test]
        public void ShouldDeserializeActionBinModify()
        {
            var json = "{" +
                "\"DataParameters\":[{\"Adress\":\"100\",\"Endian\":\"BigEndian\",\"Type\":\"U16\",\"Value\":0}]," +
                "\"To\":1," +
                "\"Title\":\"Test ActionBinModify\"," +
                "\"Todo\":true}";

            var result = JsonConvert.DeserializeObject<ActionBinModify>(json);

            result.DataParameters.ForEach(x => x.CheckAdress());

            Assert.That(result.Title, Is.EqualTo("Test ActionBinModify"));
            Assert.That(result.To, Is.EqualTo(1));
            Assert.That(result.Todo, Is.EqualTo(true));
            Assert.That(result.DataParameters[0].Adress, Is.EqualTo(100));
            Assert.That(result.DataParameters[0].Origin, Is.EqualTo(SeekOrigin.Begin));
            Assert.That(result.DataParameters[0].Endian, Is.EqualTo(EnumEndianType.BigEndian));
            Assert.That(result.DataParameters[0].Type, Is.EqualTo(EnumDataType.U16));
            Assert.That(result.DataParameters[0].Value, Is.EqualTo(0));
        }

        /// <summary>
        /// Test the deserialization of a DataParameters.
        /// </summary>
        [Test]
        public void ShouldDeserializeDataParameters()
        {
            var json = "{" +
                "\"Adress\":\"100\"," +
                "\"Endian\":\"BigEndian\"," +
                "\"Type\":\"U16\"," +
                "\"Value\":0}";

            var result = JsonConvert.DeserializeObject<DataParameters>(json);

            _ = result.CheckAdress();

            Assert.That(result.Adress, Is.EqualTo(100));
            Assert.That(result.Origin, Is.EqualTo(SeekOrigin.Begin));
            Assert.That(result.Endian, Is.EqualTo(EnumEndianType.BigEndian));
            Assert.That(result.Type, Is.EqualTo(EnumDataType.U16));
            Assert.That(result.Value, Is.EqualTo(0));
        }

        /// <summary>
        /// Test the deserialization of a ImagePattern.
        /// </summary>
        [Test]
        public void ShouldDeserializeImagePattern()
        {
            var json = "{" +
                "\"Height\":96," +
                "\"Format\":\"Planar-2BPP\"," +
                "\"Width\":128}";

            var result = JsonConvert.DeserializeObject<ImagePattern>(json);

            Assert.That(result.Height, Is.EqualTo(96));
            Assert.That(result.Width, Is.EqualTo(128));
            Assert.That(result.Format, Is.EqualTo("Planar-2BPP"));
        }

        /// <summary>
        /// Test the deserialization of an ImagePattern palette.
        /// </summary>
        [Test]
        public void ShouldDeserializeImagePatternPalette()
        {
            var json = "{\"Palette\":[{\"Alpha\":255,\"Red\":0,\"Green\":0,\"Blue\":0},{\"Alpha\":255,\"Red\":8,\"Green\":82,\"Blue\":66},{\"Alpha\":255,\"Red\":90,\"Green\":140,\"Blue\":255},{\"Alpha\":255,\"Red\":239,\"Green\":239,\"Blue\":239}]}";

            // ITraceWriter traceWriter = new MemoryTraceWriter();
            var result = JsonConvert.DeserializeObject<ImagePattern>(json);

            // var result = JsonConvert.DeserializeObject<ImageParameters>(json, new JsonSerializerSettings { TraceWriter = traceWriter });
            // Console.WriteLine(traceWriter);
            Assert.That(result.Palette[0], Is.EqualTo(new SKColor(0x00, 0x00, 0x00, 0xFF)));
            Assert.That(result.Palette[1], Is.EqualTo(new SKColor(0x08, 0x52, 0x42, 0xFF)));
            Assert.That(result.Palette[2], Is.EqualTo(new SKColor(0x5A, 0x8C, 0xFF, 0xFF)));
            Assert.That(result.Palette[3], Is.EqualTo(new SKColor(0xEF, 0xEF, 0xEF, 0xFF)));
        }

        /// <summary>
        /// Test the deserialization of a TilePattern.
        /// </summary>
        [Test]
        public void ShouldDeserializeTilePattern()
        {
            var json = "{" +
                "\"Height\":8," +
                "\"Interleave\":true," +
                "\"Map\":[[0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,4,4,4,4,4,4,4,4,6,6,6,6,6,6,6,6,8,8,8,8,8,8,8,8,10,10,10,10,10,10,10,10,12,12,12,12,12,12,12,12,14,14,14,14,14,14,14,14],[1,1,1,1,1,1,1,1,3,3,3,3,3,3,3,3,5,5,5,5,5,5,5,5,7,7,7,7,7,7,7,7,9,9,9,9,9,9,9,9,11,11,11,11,11,11,11,11,13,13,13,13,13,13,13,13,15,15,15,15,15,15,15,15]]," +
                "\"Order\":\"Planar\"," +
                "\"Size\":16," +
                "\"Width\":8}";

            var result = JsonConvert.DeserializeObject<TilePattern>(json);

            Assert.That(result.Height, Is.EqualTo(8));
            Assert.That(result.Interleave, Is.True);
            Assert.That(result.Width, Is.EqualTo(8));
            Assert.That(result.Size, Is.EqualTo(16));
            Assert.That(result.Order, Is.EqualTo(EnumTileOrder.Planar));
            Assert.That(result.Map, Is.EqualTo(new List<List<short>>()
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
            }));
        }
    }
}