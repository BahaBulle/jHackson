// <copyright file="PluginJsonConverter.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.JsonConverters
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Reflection;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class PluginJsonConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return true;
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
                var assembly = Assembly.LoadFrom(jToken.Value<string>());

                var typeModule = assembly.GetTypes().FirstOrDefault(x => typeof(IPlugin).IsAssignableFrom(x));

                if (typeModule != null)
                {
                    var methodInfo = typeModule.GetMethod(PluginsHelper.INIT_MODULE_METHOD_NAME);

                    if (methodInfo != null)
                    {
                        var o = Activator.CreateInstance(typeModule);

                        _ = methodInfo.Invoke(o, null);

                        list.Add(jToken.Value<string>());
                    }
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