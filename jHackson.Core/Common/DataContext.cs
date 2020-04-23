using jHackson.Core.Exceptions;
using jHackson.Core.FileFormat;
using jHackson.Core.Localization;
using jHackson.Core.TableElements;
using System;
using System.Collections.Generic;

namespace jHackson.Core.Common
{
    public static class DataContext
    {
        public static readonly Dictionary<string, Type> ListActions = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> ListScriptFormats = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> ListTableElements = new Dictionary<string, Type>();

        public static void AddAction(string name, Type type)
        {
            if (ListActions.ContainsKey(name.ToLower()))
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionAlreadyExists", name));

            ListActions.Add(name.ToLower(), type);
        }

        public static void AddFormat(string name, Type type)
        {
            if (ListScriptFormats.ContainsKey(name.ToLower()))
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatAlreadyExists", name));

            ListScriptFormats.Add(name.ToLower(), type);
        }

        public static void AddTableElement(string identifier, Type type)
        {
            if (ListTableElements.ContainsKey(identifier.ToUpper()))
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementAlreadyExists", identifier.ToUpper()));

            ListTableElements.Add(identifier.ToUpper(), type);
        }

        public static bool FormatExists(string key)
        {
            return ListScriptFormats.ContainsKey(key.ToLower());
        }

        public static Type GetAction(string key)
        {
            if (ListActions.ContainsKey(key))
                return ListActions[key];

            return null;
        }

        public static Dictionary<string, Type> GetActions()
        {
            return ListActions;
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