namespace JHackson.Tests.Json
{
    using System.Collections.Generic;
    using JHackson.Actions.Binary;
    using JHackson.Core.Actions;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SkiaSharp;

    /// <summary>
    /// Provides a class to test serialization and deserialization.
    /// </summary>
    public class JsonTest
    {
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

            //ITraceWriter traceWriter = new MemoryTraceWriter();

            var result = JsonConvert.DeserializeObject<ImagePattern>(json);
            //var result = JsonConvert.DeserializeObject<ImageParameters>(json, new JsonSerializerSettings { TraceWriter = traceWriter });

            //Console.WriteLine(traceWriter);

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
        /// Test the serialization of the action Save in bin mode.
        /// </summary>
        [Test]
        public void ShouldSerializeActionBinSave()
        {
            var expected = "{" +
                "\"FileName\":\"fichier.txt\"," +
                "\"Format\":\"Bin\"," +
                "\"From\":1," +
                "\"ImageParameters\":null," +
                "\"Source\":{\"AdressEnd\":null,\"AdressStart\":null,\"Size\":null}," +
                "\"Title\":\"Test ActionBinSave\"," +
                "\"Todo\":true}";

            var action = new ActionSave()
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