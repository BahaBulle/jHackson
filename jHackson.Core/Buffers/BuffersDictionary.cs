// <copyright file="BuffersDictionary.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Buffers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;

    public class BuffersDictionary : IDictionary<int, object>
    {
        private readonly Dictionary<int, object> listBuffers = new Dictionary<int, object>();

        /// <inheritdoc/>
        public int Count => this.listBuffers.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((IDictionary<int, object>)this.listBuffers).IsReadOnly;

        /// <inheritdoc/>
        public ICollection<int> Keys => this.listBuffers.Keys;

        /// <inheritdoc/>
        public ICollection<object> Values => this.listBuffers.Values;

        /// <inheritdoc/>
        public object this[int key]
        {
            get
            {
                if (!this.listBuffers.ContainsKey(key))
                {
                    this.Add(key, new MemoryStream());
                }
                else if (!this.listBuffers.ContainsKey(key))
                {
                    throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", key));
                }

                return this.listBuffers[key];
            }
            set => this.listBuffers[key] = value;
        }

        /// <inheritdoc/>
        public void Add(int key, object obj)
        {
            if (this.listBuffers.ContainsKey(key))
            {
                if (this.listBuffers[key] is MemoryStream ms)
                {
                    ms.Close();
                    ms.Dispose();
                }

                this.listBuffers[key] = null;

                this.listBuffers.Remove(key);
            }

            this.listBuffers.Add(key, obj);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<int, object> item)
        {
            if (this.listBuffers.ContainsKey(item.Key))
            {
                if (this.listBuffers[item.Key] is MemoryStream ms)
                {
                    ms.Close();
                    ms.Dispose();
                }

                this.listBuffers[item.Key] = null;

                this.listBuffers.Remove(item.Key);
            }

            ((IDictionary<int, object>)this.listBuffers).Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.listBuffers.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<int, object> item)
        {
            return ((IDictionary<int, object>)this.listBuffers).Contains(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(int key)
        {
            return this.listBuffers.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<int, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a specific buffer as MemoryStream.
        /// </summary>
        /// <param name="key">Id of the buffer to get.</param>
        /// <param name="forceCreation">Force creation of the buffer if not exists.</param>
        /// <returns>The buffer as a MemoryStream.</returns>
        public T Get<T>(int key, bool forceCreation = false)
        {
            if (!this.listBuffers.ContainsKey(key) && forceCreation)
            {
                this.Add(key, new MemoryStream());
            }

            if (!this.listBuffers.ContainsKey(key))
            {
                throw new JHacksonException(LocalizationManager.GetMessage("core.bufferUnknow", key));
            }

            return this.listBuffers[key] is T buffer
                ? buffer
                : throw new JHacksonException(LocalizationManager.GetMessage("core.bufferIncorrectFormat", key, typeof(T).Name));
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<int, object>> GetEnumerator()
        {
            return ((IDictionary<int, object>)this.listBuffers).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.listBuffers.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(int key)
        {
            return this.listBuffers.Remove(key);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<int, object> item)
        {
            return ((IDictionary<int, object>)this.listBuffers).Remove(item);
        }

        /// <inheritdoc/>
        public bool TryGetValue(int key, [MaybeNullWhen(false)] out object value)
        {
            return this.listBuffers.TryGetValue(key, out value);
        }
    }
}