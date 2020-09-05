﻿// <copyright file="PluginsHelper.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using JHackson.Core.Actions;
    using JHackson.Core.FileFormat;
    using JHackson.Core.ImageFormat;
    using JHackson.Core.TableElements;

    /// <summary>
    /// Helper class to manage plugins.
    /// </summary>
    public static class PluginsHelper
    {
        private const string CHARACTERVARIABLE = "$";

        private const string PLUGINSDIRECTORY = "Plugins";

        private static readonly Regex RegexParameter = new Regex("#([a-zA-Z0-9]+)#");

        /// <summary>
        /// Load plugins in "Plugins" directory.
        /// </summary>
        public static void LoadPlugins()
        {
            if (Directory.Exists(PLUGINSDIRECTORY))
            {
                string[] filesList = Directory.GetFiles(PLUGINSDIRECTORY, "*.dll");

                if (filesList.Length > 0)
                {
                    foreach (string fileName in filesList)
                    {
                        var assembly = Assembly.LoadFrom(fileName);

                        foreach (var t in assembly.GetExportedTypes())
                        {
                            if (t.GetInterface("IActionJson", true) != null)
                            {
                                LoadAction(t);
                            }
                            else if (t.GetInterface("IFileFormat", true) != null)
                            {
                                LoadFileFormat(t);
                            }
                            else if (t.GetInterface("ITableElement", true) != null && !t.IsAbstract)
                            {
                                LoadTableElement(t);
                            }
                            else if (t.GetInterface("IImageFormat", true) != null)
                            {
                                LoadImageFormat(t);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replace properties parameters with their values.
        /// </summary>
        /// <param name="action">Action which have the property.</param>
        /// <param name="text">Text containing parameters.</param>
        /// <returns>Return a string with all parameters replaced by their values.</returns>
        public static string ReplaceParameters(IActionJson action, string text)
        {
            if (action == null || string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string result = text;

            var list = RegexParameter.Matches(result);

            foreach (Match m in list)
            {
                var obj = action
                    .GetType()
                    .GetProperty(m.Groups[1].Value);

                if (obj.GetValue(action) != null)
                {
                    result = result.Replace(m.Groups[0].Value, obj.GetValue(action).ToString(), StringComparison.InvariantCulture);
                }
            }

            return result;
        }

        /// <summary>
        /// Replace variables name by their values.
        /// </summary>
        /// <param name="variablesList">List of variables in the project.</param>
        /// <param name="text">Text containing variables to replace.</param>
        /// <returns>Return string with all variables replaced by their values.</returns>
        public static string ReplaceVariables(Dictionary<string, string> variablesList, string text)
        {
            if (variablesList == null || string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string result = text;

            foreach (var variable in variablesList)
            {
                if (result.Contains(CHARACTERVARIABLE + variable.Key + CHARACTERVARIABLE, StringComparison.InvariantCulture))
                {
                    result = result.Replace(CHARACTERVARIABLE + variable.Key + CHARACTERVARIABLE, variable.Value, StringComparison.InvariantCulture);
                }
            }

            return result;
        }

        private static void LoadAction(Type elementType)
        {
            var action = (IActionJson)elementType.InvokeMember(
                null,
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null,
                CultureInfo.InvariantCulture);

            if (action != null)
            {
                DataContext.AddAction(action.Name, elementType);
            }
        }

        private static void LoadFileFormat(Type elementType)
        {
            var format = (IFileFormat)elementType.InvokeMember(
                null,
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null,
                CultureInfo.InvariantCulture);

            if (format != null)
            {
                DataContext.AddFileFormat(format.Name, elementType);
            }
        }

        private static void LoadImageFormat(Type elementType)
        {
            var format = (IImageFormat)elementType.InvokeMember(
                null,
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null,
                CultureInfo.InvariantCulture);

            if (format != null)
            {
                DataContext.AddImageFormat(format.Name, elementType);
            }
        }

        private static void LoadTableElement(Type elementType)
        {
            var element = (ITableElement)elementType.InvokeMember(
                null,
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                null,
                null,
                CultureInfo.InvariantCulture);

            if (element != null)
            {
                DataContext.AddTableElement(element.Name, elementType);
            }
        }
    }
}