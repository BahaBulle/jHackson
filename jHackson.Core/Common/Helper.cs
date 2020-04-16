using jHackson.Core.Actions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace jHackson.Core.Common
{
    public static class Helper
    {
        private const string CHARACTER_VARIABLE = "$";

        private static readonly Regex _regexParameter = new Regex("#([a-zA-Z0-9]+)#");

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