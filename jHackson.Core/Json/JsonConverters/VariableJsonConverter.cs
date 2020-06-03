// <copyright file="VariableJsonConverter.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.JsonConverters
{
    using System;
    using System.Collections;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

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
            throw new InvalidOperationException(LocalizationManager.GetMessage("core.serialization.serializationError"));
        }
    }
}