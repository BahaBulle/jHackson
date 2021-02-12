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

        /// <inheritdoc/>
        public int Count => this.listVariables.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((IDictionary<string, string>)this.listVariables).IsReadOnly;

        /// <inheritdoc/>
        public ICollection<string> Keys => this.listVariables.Keys;

        /// <inheritdoc/>
        public ICollection<string> Values => this.listVariables.Values;

        /// <inheritdoc/>
        public string this[string key]
        {
            get => this.listVariables[key];
            set => this.listVariables[key] = value;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Clear()
        {
            this.listVariables.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)this.listVariables).Contains(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return this.listVariables.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)this.listVariables).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listVariables.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(string key)
        {
            return this.listVariables.Remove(key);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
        {
            return this.listVariables.TryGetValue(key, out value);
        }
    }
}