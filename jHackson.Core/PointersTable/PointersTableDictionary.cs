// <copyright file="PointersTableDictionary.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.PointersTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;

    /// <summary>
    /// Represents a collection of PointersTable.
    /// </summary>
    public class PointersTableDictionary : IDictionary<int, IPointersTable>
    {
        private readonly Dictionary<int, IPointersTable> listTables = new Dictionary<int, IPointersTable>();

        /// <inheritdoc/>
        public int Count => this.listTables.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((IDictionary<int, IPointersTable>)this.listTables).IsReadOnly;

        /// <inheritdoc/>
        public ICollection<int> Keys => this.listTables.Keys;

        /// <inheritdoc/>
        public ICollection<IPointersTable> Values => this.listTables.Values;

        /// <inheritdoc/>
        public IPointersTable this[int key]
        {
            get => this.listTables.ContainsKey(key)
                    ? this.listTables[key]
                    : throw new JHacksonException(LocalizationManager.GetMessage("core.tableUnknow", key));
            set => this.listTables[key] = value;
        }

        /// <inheritdoc/>
        public void Add(int key, IPointersTable table)
        {
            if (this.listTables.ContainsKey(key))
            {
                this.listTables[key] = null;
                this.listTables.Remove(key);
            }

            this.listTables.Add(key, table);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<int, IPointersTable> item)
        {
            if (this.Contains(item))
            {
                this.listTables.Remove(item.Key);
            }

            ((IDictionary<int, IPointersTable>)this.listTables).Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.listTables.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<int, IPointersTable> item)
        {
            return ((IDictionary<int, IPointersTable>)this.listTables).Contains(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(int key)
        {
            return this.listTables.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<int, IPointersTable>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a specific pointers table.
        /// </summary>
        /// <param name="key">Id of the buffer to get.</param>
        /// <returns>The pointers table.</returns>
        public IPointersTable Get(int key)
        {
            return !this.listTables.ContainsKey(key)
                ? throw new JHacksonException(LocalizationManager.GetMessage("core.pointersTableUnknow", key))
                : this.listTables[key];
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<int, IPointersTable>> GetEnumerator()
        {
            return ((IDictionary<int, IPointersTable>)this.listTables).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listTables.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(int key)
        {
            return this.listTables.Remove(key);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<int, IPointersTable> item)
        {
            return ((IDictionary<int, IPointersTable>)this.listTables).Remove(item);
        }

        /// <inheritdoc/>
        public bool TryGetValue(int key, [MaybeNullWhen(false)] out IPointersTable value)
        {
            return this.listTables.TryGetValue(key, out value);
        }
    }
}