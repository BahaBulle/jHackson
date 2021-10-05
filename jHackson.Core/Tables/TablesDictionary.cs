// <copyright file="TablesDictionary.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Tables
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Text.Tables;

    /// <summary>
    /// Representes o collection of Table.
    /// </summary>
    public class TablesDictionary : IDictionary<string, ITable>
    {
        private readonly Dictionary<string, ITable> listTables = new Dictionary<string, ITable>();

        /// <inheritdoc/>
        public int Count => this.listTables.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((IDictionary<string, ITable>)this.listTables).IsReadOnly;

        /// <inheritdoc/>
        public ICollection<string> Keys => this.listTables.Keys;

        /// <inheritdoc/>
        public ICollection<ITable> Values => this.listTables.Values;

        /// <inheritdoc/>
        public ITable this[string key]
        {
            get => this.listTables.ContainsKey(key)
                    ? this.listTables[key]
                    : throw new JHacksonException(LocalizationManager.GetMessage("core.tableUnknow", key));
            set => this.listTables[key] = value;
        }

        /// <inheritdoc/>
        public void Add(string key, ITable table)
        {
            if (this.listTables.ContainsKey(key))
            {
                this.listTables[key] = null;
                this.listTables.Remove(key);
            }

            this.listTables.Add(key, table);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<string, ITable> item)
        {
            if (this.Contains(item))
            {
                this.listTables.Remove(item.Key);
            }

            ((IDictionary<string, ITable>)this.listTables).Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.listTables.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, ITable> item)
        {
            return ((IDictionary<string, ITable>)this.listTables).Contains(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return this.listTables.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, ITable>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, ITable>> GetEnumerator()
        {
            return ((IDictionary<string, ITable>)this.listTables).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listTables.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(string key)
        {
            return this.listTables.Remove(key);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, ITable> item)
        {
            return ((IDictionary<string, ITable>)this.listTables).Remove(item);
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out ITable value)
        {
            return this.listTables.TryGetValue(key, out value);
        }
    }
}