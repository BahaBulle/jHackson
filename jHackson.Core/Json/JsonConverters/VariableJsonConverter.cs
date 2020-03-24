using jHackson.Core.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace jHackson.Core.Json.JsonConverters
{
    public class VariableJsonConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IActionVariable);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonToken = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as IList;

            foreach (var jToken in jsonToken.Children())
            {
                var newObject = new ActionVariable();
                serializer.Populate(jToken.CreateReader(), newObject);

                list.Add(newObject);
            }

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}