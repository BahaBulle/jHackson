namespace JHackson.Tests.Json
{
    using JHackson.Actions;
    using JHackson.Core.Actions;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SkiaSharp;

    public class Actions
    {
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

        [Test]
        public void ShouldSerializeActionSave()
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

        [Test]
        public void ShouldSerializeImageParameters()
        {
            var expected = "{" +
                "\"Format\":\"2BPP GB\"," +
                "\"Height\":96," +
                "\"Palette\":[{\"Alpha\":255,\"Red\":0,\"Green\":0,\"Blue\":0,\"Hue\":0.0},{\"Alpha\":255,\"Red\":8,\"Green\":82,\"Blue\":66,\"Hue\":167.02704},{\"Alpha\":255,\"Red\":90,\"Green\":140,\"Blue\":255,\"Hue\":221.8182},{\"Alpha\":255,\"Red\":239,\"Green\":239,\"Blue\":239,\"Hue\":0.0}]," +
                "\"TileHeight\":8," +
                "\"TileWidth\":8," +
                "\"Width\":128}";

            //"{\"Format\":\"2BPP GB\",\"Height\":96,\"Palette\":[{\"Alpha\":255,\"Red\":0,\"Green\":0,\"Blue\":0,\"Hue\":0.0},{\"Alpha\":255,\"Red\":8,\"Green\":82,\"Blue\":66,\"Hue\":167.02704},{\"Alpha\":255,\"Red\":90,\"Green\":140,\"Blue\":255,\"Hue\":221.8182},{\"Alpha\":255,\"Red\":239,\"Green\":239,\"Blue\":239,\"Hue\":0.0}],\"TileHeight\":8,\"TileWidth\":8,\"Width\":128}"

            var parameters = new ImageParameters()
            {
                Width = 128,
                Height = 96,
                Format = "2BPP GB",
                TileHeight = 8,
                TileWidth = 8,
            };
            parameters.Palette.Add(new SKColor(0x00, 0x00, 0x00, 0xFF));
            parameters.Palette.Add(new SKColor(0x08, 0x52, 0x42, 0xFF));
            parameters.Palette.Add(new SKColor(0x5A, 0x8C, 0xFF, 0xFF));
            parameters.Palette.Add(new SKColor(0xEF, 0xEF, 0xEF, 0xFF));

            var result = JsonConvert.SerializeObject(parameters);

            Assert.That(expected, Is.EqualTo(result));
        }
    }
}