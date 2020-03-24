using jHackson.Core.Exceptions;
using jHackson.Core.FileFormat;
using System;
using System.Collections.Generic;

namespace jHackson.Core.Common
{
    public static class DataContext
    {
        #region Properties

        public static readonly Dictionary<string, Type> ListMethods = new Dictionary<string, Type>();

        private static readonly Dictionary<string, Type> ListScriptFormats = new Dictionary<string, Type>();

        #endregion

        #region Methods for ListMethods property

        public static void AddMethod(string name, Type type)
        {
            if (ListMethods.ContainsKey(name.ToLower()))
                throw new JHacksonException($"(EE) Method {name} already exists!");

            ListMethods.Add(name.ToLower(), type);
        }

        public static Type GetMethod(string key)
        {
            if (ListMethods.ContainsKey(key))
                return ListMethods[key];

            return null;
        }

        public static Dictionary<string, Type> GetMethods()
        {
            return ListMethods;
        }

        #endregion

        #region Methods for ListScriptFormats

        public static void AddFormat(string name, Type type)
        {
            if (ListScriptFormats.ContainsKey(name.ToLower()))
                throw new JHacksonException($"(EE) Format {name} already exists!");

            ListScriptFormats.Add(name.ToLower(), type);
        }

        public static IFileFormat GetFormat(string key)
        {
            if (ListScriptFormats.ContainsKey(key.ToLower()))
                return Activator.CreateInstance(ListScriptFormats[key.ToLower()]) as IFileFormat;

            return null;
        }

        public static Dictionary<string, Type> GetFormats()
        {
            return ListScriptFormats;
        }

        #endregion
    }
}