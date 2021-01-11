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

    /// <summary>
    /// Class that contains all possible class from plugins.
    /// </summary>
    public static class DataContext
    {
        /// <summary>
        /// List of possible actions.
        /// </summary>
        public static readonly Dictionary<string, Type> ListActions = new Dictionary<string, Type>();

        /// <summary>
        /// List of possible files formats.
        /// </summary>
        private static readonly Dictionary<string, Type> ListFileFormats = new Dictionary<string, Type>();

        /// <summary>
        ///  List of possible images formats.
        /// </summary>
        private static readonly Dictionary<string, Type> ListImageFormats = new Dictionary<string, Type>();

        /// <summary>
        /// List of possible table elements.
        /// </summary>
        private static readonly Dictionary<string, Type> ListTableElements = new Dictionary<string, Type>();

        /// <summary>
        /// Add Type of action in the dictionary of possible actions.
        /// </summary>
        /// <param name="name">Name of the action.</param>
        /// <param name="type">Type of the action.</param>
        public static void AddAction(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionNotSpecified"));
            }

            if (ListActions.ContainsKey(name.ToUpper(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.actionAlreadyExists", name));
            }

            ListActions.Add(name.ToUpper(CultureInfo.CurrentCulture), type);
        }

        /// <summary>
        /// Add file format in the dictionary of possible file formats.
        /// </summary>
        /// <param name="name">Name of the format.</param>
        /// <param name="type">Type of the format.</param>
        public static void AddFileFormat(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.fileFormatNotSpecified"));
            }

            if (ListFileFormats.ContainsKey(name.ToLower(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.fileFormatAlreadyExists", name));
            }

            ListFileFormats.Add(name.ToLower(CultureInfo.CurrentCulture), type);
        }

        /// <summary>
        /// Add image format in the dictionary of possible image formats.
        /// </summary>
        /// <param name="name">Name of the format.</param>
        /// <param name="type">Type of the format.</param>
        public static void AddImageFormat(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.imageFormatNotSpecified"));
            }

            if (ListImageFormats.ContainsKey(name.ToLower(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.imageFormatAlreadyExists", name));
            }

            ListImageFormats.Add(name.ToLower(CultureInfo.CurrentCulture), type);
        }

        /// <summary>
        /// Add table element in the dictionary of possible table elements.
        /// </summary>
        /// <param name="name">Name of the table element.</param>
        /// <param name="type">Type of the table element.</param>
        public static void AddTableElement(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementNotSpecified"));
            }

            if (ListTableElements.ContainsKey(name.ToUpper(CultureInfo.CurrentCulture)))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.tableElementAlreadyExists", name.ToUpper(CultureInfo.CurrentCulture)));
            }

            ListTableElements.Add(name.ToUpper(CultureInfo.CurrentCulture), type);
        }

        /// <summary>
        /// Check if the file format exists in the dictionary.
        /// </summary>
        /// <param name="key">Name of the file format.</param>
        /// <returns>Returns true if exists, false otherwise.</returns>
        public static bool FileFormatExists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.formatNotSpecified"));
            }

            return ListFileFormats.ContainsKey(key.ToLower(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Get the type of an action.
        /// </summary>
        /// <param name="key">Name of the action.</param>
        /// <returns>Returns the Type of the action.</returns>
        public static Type GetAction(string key)
        {
            return ListActions.ContainsKey(key) ? ListActions[key] : null;
        }

        /// <summary>
        /// Get an instance of a FileFormat.
        /// </summary>
        /// <param name="key">Name of the FileFormat.</param>
        /// <returns>Returns an instance of the FileFormat.</returns>
        public static IFileFormat GetFileFormat(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.fileFormatNotSpecified"));
            }

            return ListFileFormats.ContainsKey(key.ToLower(CultureInfo.CurrentCulture))
                ? Activator.CreateInstance(ListFileFormats[key.ToLower(CultureInfo.CurrentCulture)]) as IFileFormat
                : null;
        }

        /// <summary>
        /// Get the type of an ImageFormat.
        /// </summary>
        /// <param name="name">Name of the ImageFormat.</param>
        /// <returns>Returns the type of the ImageFormat.</returns>
        public static Type GetImageFormat(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.imageFormatNotSpecified"));
            }

            return ListImageFormats.ContainsKey(name.ToLower(CultureInfo.CurrentCulture))
                ? ListImageFormats[name.ToLower(CultureInfo.CurrentCulture)]
                : null;
        }

        /// <summary>
        /// Get an instance of a TableElement.
        /// </summary>
        /// <param name="key">Name of the TableElement.</param>
        /// <returns>Returns an instance of the TableElement.</returns>
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

        /// <summary>
        /// Get the dictionary of possible TableElements.
        /// </summary>
        /// <returns>Returns the dictionary of possible TableElements.</returns>
        public static Dictionary<string, Type> GetTableElements()
        {
            return ListTableElements;
        }
    }
}