using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace jHackson.Core.Json.JsonConverters
{
    public class ActionJsonConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IActionJson);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonToken = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as IList;

            foreach (var jToken in jsonToken.Children())
            {
                JToken childToken = null;

                // Search Name property
                foreach (var child in jToken.Values())
                {
                    if (((JProperty)child.Parent).Name == "Name")
                    {
                        childToken = child;
                        break;
                    }
                }

                if (childToken == null)
                    throw new JHacksonException($"Name property not found in '{jToken.ToString()}'");

                // Create Action object
                if (DataContext.ListMethods.ContainsKey(childToken.ToString().ToLower()))
                {
                    IActionJson newObject = Activator.CreateInstance(DataContext.GetMethod(childToken.ToString().ToLower())) as IActionJson;
                    serializer.Populate(jToken.CreateReader(), newObject);

                    list.Add(newObject);
                }
                else
                    throw new JHacksonException($"Unkwnow method '{childToken.ToString()}'");
            }

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}