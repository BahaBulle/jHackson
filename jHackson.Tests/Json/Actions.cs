using JHackson.Actions;
using JHackson.Core.Actions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JHackson.Tests.Json
{
    public class Actions
    {
        [Test]
        public void ShouldSerializeActionBinaryCopy()
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
                "\"Title\":\"Test ActionFileLoad\"," +
                "\"Todo\":true}";

            var action = new ActionBinCopy()
            {
                Destination = new BufferParameters() { AdressStart = 0x100, AdressEnd = 0x300, Size = 0x200 },
                From = 1,
                Source = new BufferParameters() { AdressStart = 0, AdressEnd = 0x100, Size = 0x100 },
                Title = "Test ActionFileLoad",
                To = 2,
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void ShouldSerializeActionFileLoad()
        {
            var expected = "{" +
                "\"FileName\":\"fichier.txt\"," +
                "\"To\":1," +
                "\"Title\":\"Test ActionFileLoad\"," +
                "\"Todo\":true}";

            var action = new ActionFileLoad()
            {
                FileName = "fichier.txt",
                Title = "Test ActionFileLoad",
                To = 1,
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void ShouldSerializeActionFileSave()
        {
            var expected = "{" +
                "\"FileName\":\"fichier.txt\"," +
                "\"Format\":\"Bin\"," +
                "\"From\":1," +
                "\"Title\":\"Test ActionFileLoad\"," +
                "\"Todo\":true}";

            var action = new ActionBinSave()
            {
                FileName = "fichier.txt",
                Format = "Bin",
                From = 1,
                Title = "Test ActionFileLoad",
                Todo = true,
            };

            var result = JsonConvert.SerializeObject(action);

            Assert.That(expected, Is.EqualTo(result));
        }
    }
}