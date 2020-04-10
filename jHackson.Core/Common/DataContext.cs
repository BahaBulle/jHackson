using jHackson.Core.Exceptions;
using jHackson.Core.FileFormat;
using jHackson.Core.TableElements;
using System;
using System.Collections.Generic;

namespace jHackson.Core.Common
{
    public static class DataContext
    {
        public static readonly Dictionary<string, Type> ListMethods = new Dictionary<string, Type>();

        private static readonly Dictionary<string, Type> ListScriptFormats = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> ListTableElements = new Dictionary<string, Type>();

        public static void AddFormat(string name, Type type)
        {
            if (ListScriptFormats.ContainsKey(name.ToLower()))
                throw new JHacksonException($"(EE) Format {name} already exists!");

            ListScriptFormats.Add(name.ToLower(), type);
        }

        public static void AddMethod(string name, Type type)
        {
            if (ListMethods.ContainsKey(name.ToLower()))
                throw new JHacksonException($"(EE) Method {name} already exists!");

            ListMethods.Add(name.ToLower(), type);
        }

        public static void AddTableElement(string identifier, Type type)
        {
            if (ListTableElements.ContainsKey(identifier.ToUpper()))
                throw new JHacksonException($"(EE) TableElement {identifier.ToUpper()} already exists!");

            ListTableElements.Add(identifier.ToUpper(), type);
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

        public static ITableElement GetTableElement(string key)
        {
            if (ListTableElements.ContainsKey(key.ToUpper()))
                return Activator.CreateInstance(ListTableElements[key.ToUpper()]) as ITableElement;

            return null;
        }

        public static Dictionary<string, Type> GetTableElements()
        {
            return ListTableElements;
        }
    }
}