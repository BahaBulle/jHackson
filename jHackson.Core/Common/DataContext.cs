// <copyright file="DataContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Common
{
    using System;
    using System.Collections.Generic;
    using JHackson.Core.Exceptions;
    using JHackson.Core.FileFormat;
    using JHackson.Core.Localization;
    using JHackson.Core.TableElements;

    public static class DataContext
    {
        public static readonly Dictionary<string, Type> ListActions = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> ListScriptFormats = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> ListTableElements = new Dictionary<string, Type>();

        public static void AddAction(string name, Type type)
        {
            if (ListActions.ContainsKey(name.ToLower()))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionAlreadyExists", name));
            }

            ListActions.Add(name.ToLower(), type);
        }

        public static void AddFormat(string name, Type type)
        {
            if (ListScriptFormats.ContainsKey(name.ToLower()))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatAlreadyExists", name));
            }

            ListScriptFormats.Add(name.ToLower(), type);
        }

        public static void AddTableElement(string identifier, Type type)
        {
            if (ListTableElements.ContainsKey(identifier.ToUpper()))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementAlreadyExists", identifier.ToUpper()));
            }

            ListTableElements.Add(identifier.ToUpper(), type);
        }

        public static bool FormatExists(string key)
        {
            return ListScriptFormats.ContainsKey(key.ToLower());
        }

        public static Type GetAction(string key)
        {
            return ListActions.ContainsKey(key) ? ListActions[key] : null;
        }

        public static Dictionary<string, Type> GetActions()
        {
            return ListActions;
        }

        public static IFileFormat GetFormat(string key)
        {
            return ListScriptFormats.ContainsKey(key.ToLower())
                ? Activator.CreateInstance(ListScriptFormats[key.ToLower()]) as IFileFormat
                : null;
        }

        public static Dictionary<string, Type> GetFormats()
        {
            return ListScriptFormats;
        }

        public static ITableElement GetTableElement(string key)
        {
            return ListTableElements.ContainsKey(key.ToUpper())
                ? Activator.CreateInstance(ListTableElements[key.ToUpper()]) as ITableElement
                : null;
        }

        public static Dictionary<string, Type> GetTableElements()
        {
            return ListTableElements;
        }
    }
}