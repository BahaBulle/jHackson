// <copyright file="ActionJsonConverter.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.JsonConverters
{
    using System;
    using System.Collections;
    using System.Globalization;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

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
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("core.serialization.propertyNameUnknow", jToken.ToString()));
                }

                // Create Action object
                if (DataContext.ListActions.ContainsKey(childToken.ToString().ToUpper(CultureInfo.InvariantCulture)))
                {
                    var newObject = Activator.CreateInstance(DataContext.GetAction(childToken.ToString().ToUpper(CultureInfo.InvariantCulture))) as IActionJson;
                    serializer.Populate(jToken.CreateReader(), newObject);

                    list.Add(newObject);
                }
                else
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("core.serialization.actionUnknow", childToken.ToString()));
                }
            }

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException(LocalizationManager.GetMessage("core.serialization.serializationError"));
        }
    }
}