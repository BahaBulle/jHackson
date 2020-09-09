// <copyright file="SKColorJsonConverter.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.JsonConverters
{
    using System;
    using System.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using SkiaSharp;

    public class SKColorJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SKColor);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonToken = JToken.Load(reader);

            var list = Activator.CreateInstance(objectType) as IList;

            foreach (var jToken in jsonToken.Children())
            {
                byte? alpha = 0;
                byte? red = 0;
                byte? green = 0;
                byte? blue = 0;

                foreach (var token in jToken.Children())
                {
                    var property = token as JProperty;

                    switch (property.Name)
                    {
                        case "Alpha":
                            alpha = (byte)(long)((JValue)property.Value).Value;
                            break;

                        case "Red":
                            red = (byte)(long)((JValue)property.Value).Value;
                            break;

                        case "Green":
                            green = (byte)(long)((JValue)property.Value).Value;
                            break;

                        case "Blue":
                            blue = (byte)(long)((JValue)property.Value).Value;
                            break;
                    }
                }

                if (alpha.HasValue && red.HasValue && green.HasValue && blue.HasValue)
                {
                    var color = new SKColor(red.Value, green.Value, blue.Value, alpha.Value);

                    list.Add(color);
                }
            }

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}