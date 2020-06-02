// <copyright file="DataContext.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionNotSpecified"));
            }

            if (ListActions.ContainsKey(name.ToLower(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionAlreadyExists", name));
            }

            ListActions.Add(name.ToLower(CultureInfo.CurrentCulture), type);
        }

        public static void AddFormat(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatNotSpecified"));
            }

            if (ListScriptFormats.ContainsKey(name.ToLower(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatAlreadyExists", name));
            }

            ListScriptFormats.Add(name.ToLower(CultureInfo.CurrentCulture), type);
        }

        public static void AddTableElement(string identifier, Type type)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementNotSpecified"));
            }

            if (ListTableElements.ContainsKey(identifier.ToUpper(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementAlreadyExists", identifier.ToUpper(CultureInfo.CurrentCulture)));
            }

            ListTableElements.Add(identifier.ToUpper(CultureInfo.CurrentCulture), type);
        }

        public static bool FormatExists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatNotSpecified"));
            }

            return ListScriptFormats.ContainsKey(key.ToLower(CultureInfo.CurrentCulture));
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
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatNotSpecified"));
            }

            return ListScriptFormats.ContainsKey(key.ToLower(CultureInfo.CurrentCulture))
                ? Activator.CreateInstance(ListScriptFormats[key.ToLower(CultureInfo.CurrentCulture)]) as IFileFormat
                : null;
        }

        public static Dictionary<string, Type> GetFormats()
        {
            return ListScriptFormats;
        }

        public static ITableElement GetTableElement(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementNotSpecified"));
            }

            return ListTableElements.ContainsKey(key.ToUpper(CultureInfo.CurrentCulture))
                ? Activator.CreateInstance(ListTableElements[key.ToUpper(CultureInfo.CurrentCulture)]) as ITableElement
                : null;
        }

        public static Dictionary<string, Type> GetTableElements()
        {
            return ListTableElements;
        }
    }
}