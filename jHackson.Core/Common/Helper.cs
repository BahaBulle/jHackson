using jHackson.Core.Actions;
using jHackson.Core.FileFormat;
using jHackson.Core.TableElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace jHackson.Core.Common
{
    public static class Helper
    {
        private const string CHARACTER_VARIABLE = "$";
        private const string PLUGINS_DIRECTORY = "Plugins";

        private static readonly Regex _regexParameter = new Regex("#([a-zA-Z0-9]+)#");

        public static void LoadPlugins()
        {
            if (Directory.Exists(PLUGINS_DIRECTORY))
            {
                var filesList = Directory.GetFiles(PLUGINS_DIRECTORY, "*.dll");

                if (filesList.Length > 0)
                {
                    foreach (var fileName in filesList)
                    {
                        var assembly = Assembly.LoadFrom(fileName);

                        foreach (Type t in assembly.GetExportedTypes())
                        {
                            if (t.GetInterface("IActionJson", true) != null)
                            {
                                var action = (IActionJson)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                               BindingFlags.Public | BindingFlags.NonPublic |
                                                                               BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddMethod(action.Name, t);
                            }
                            else if (t.GetInterface("IFileFormat", true) != null)
                            {
                                var format = (IFileFormat)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                               BindingFlags.Public | BindingFlags.NonPublic |
                                                                               BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddFormat(format.Name, t);
                            }
                            else if (t.GetInterface("ITableElement", true) != null && !t.IsAbstract)
                            {
                                var element = (ITableElement)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                                  BindingFlags.Public | BindingFlags.NonPublic |
                                                                                  BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddTableElement(element.Name, t);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replace properties parameters with their values
        /// </summary>
        /// <param name="action">Action which have the property</param>
        /// <param name="text">Text containing parameters</param>
        /// <returns>Return a string with all parameters replaced by their values</returns>
        public static string ReplaceParameters(IActionJson action, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var result = text;

            var list = _regexParameter.Matches(result);

            foreach (Match m in list)
            {
                var obj = action
                    .GetType()
                    .GetProperty(m.Groups[1].Value);

                if (obj.GetValue(action) != null)
                    result = result.Replace(m.Groups[0].Value, obj.GetValue(action).ToString());
            }

            return result;
        }

        /// <summary>
        /// Replace variables name by their values
        /// </summary>
        /// <param name="variablesList">List of variables in the project</param>
        /// <param name="text">Text containing variables to replace</param>
        /// <returns>Return string with all variables replaced by their values</returns>
        public static string ReplaceVariables(Dictionary<string, string> variablesList, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string result = text;

            foreach (KeyValuePair<string, string> variable in variablesList)
            {
                if (result.Contains(CHARACTER_VARIABLE + variable.Key + CHARACTER_VARIABLE))
                    result = result.Replace(CHARACTER_VARIABLE + variable.Key + CHARACTER_VARIABLE, variable.Value);
            }

            return result;
        }
    }
}