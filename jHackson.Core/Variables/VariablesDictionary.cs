// <copyright file="VariablesDictionary.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Variables
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class VariablesDictionary : IDictionary<string, string>
    {
        private const string CHARACTERVARIABLE = "$";

        private readonly Dictionary<string, string> listVariables = new Dictionary<string, string>();

        // Gets the number
        public int Count => this.listVariables.Count;

        public bool IsReadOnly => ((IDictionary<string, string>)this.listVariables).IsReadOnly;

        public ICollection<string> Keys => this.listVariables.Keys;

        public ICollection<string> Values => this.listVariables.Values;

        public string this[string key]
        {
            get => this.listVariables[key];
            set => this.listVariables[key] = value;
        }

        /// <summary>
        /// Adds an element with the provided key and value to the System.Collections.Generic.IDictionary`2.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the System.Collections.Generic.IDictionary`2.</exception>
        /// <exception cref="NotSupportedException">The System.Collections.Generic.IDictionary`2 is read-only.</exception>
        public void Add(string key, string value)
        {
            if (this.listVariables.ContainsKey(key))
            {
                this.listVariables[key] = value;
            }
            else
            {
                this.listVariables.Add(key, value);
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            if (this.Contains(item))
            {
                this.listVariables[item.Key] = item.Value;
            }
            else
            {
                ((IDictionary<string, string>)this.listVariables).Add(item);
            }
        }

        public void Clear()
        {
            this.listVariables.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)this.listVariables).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return this.listVariables.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)this.listVariables).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listVariables.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return this.listVariables.Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)this.listVariables).Remove(item);
        }

        /// <summary>
        /// Replace all variables in a text with their value.
        /// </summary>
        /// <param name="value">Text to modify.</param>
        /// <returns>The modified value.</returns>
        public string Replace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string result = value;

            foreach (var variable in this)
            {
                if (result.Contains($"{CHARACTERVARIABLE}({variable.Key})", StringComparison.InvariantCulture))
                {
                    result = result.Replace($"{CHARACTERVARIABLE}({variable.Key})", variable.Value, StringComparison.InvariantCulture);
                }
            }

            return result;
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
        {
            return this.listVariables.TryGetValue(key, out value);
        }
    }
}