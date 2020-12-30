// <copyright file="BitList.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Image.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using JHackson.Core.Actions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;

    /// <summary>
    /// Provides a class to manage a bits list.
    /// </summary>
    public class BitList
    {
        private readonly bool isFixedSize;

        private LinkedList<bool> bitArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitList"/> class.
        /// </summary>
        public BitList()
        {
            this.isFixedSize = false;
            this.bitArray = new LinkedList<bool>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitList"/> class.
        /// </summary>
        /// <param name="bytes">The number of bit values in the new BitList.</param>
        public BitList(byte[] bytes)
            : this()
        {
            if (bytes == null)
            {
                return;
            }

            int pos = 0;
            foreach (byte value in bytes)
            {
                var bitArray = new BitArray(new byte[] { value });

                for (int i = 7; i >= 0; i--, pos++)
                {
                    this.bitArray.AddLast(bitArray.Get(i));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitList"/> class.
        /// </summary>
        /// <param name="length">The number of bit values in the new BitList.</param>
        /// <param name="defaultValue">The Boolean value to assign to each bit.</param>
        public BitList(int length, bool defaultValue)
            : this()
        {
            this.bitArray = new LinkedList<bool>(Enumerable.Repeat(defaultValue, length));
            this.isFixedSize = true;
        }

        /// <summary>
        /// Gets the number of elements contained in the BitList.
        /// </summary>
        public int Count => this.bitArray.Count;

        /// <summary>
        /// Gets or sets the value of the bit at a specific position in the BitList given by his index.
        /// </summary>
        /// <param name="index">The zero-based index of the value to get.</param>
        /// <returns>The value of the bit at position index.</returns>
        public bool this[int index]
        {
            get => this.bitArray.ElementAt(index);
            set
            {
                int position = 0;
                for (var currentNode = this.bitArray.First; currentNode != null; currentNode = currentNode.Next)
                {
                    if (position == index)
                    {
                        this.bitArray.AddBefore(currentNode, value);
                        this.bitArray.Remove(currentNode);
                        break;
                    }

                    position++;
                }
            }
        }

        /// <summary>
        /// Add a bool value at the end of the list.
        /// </summary>
        /// <param name="value">Value to added to the end od the list.</param>
        public void Add(bool value)
        {
            if (!this.isFixedSize)
            {
                this.bitArray.AddLast(value);
            }
        }

        /// <summary>
        /// Adds the elements of the specified BitList to the end of the list.
        /// </summary>
        /// <param name="bits">The BitList whose elements should be added to the end of the list.</param>
        public void AddBitList(BitList bits)
        {
            if (!this.isFixedSize && bits != null)
            {
                foreach (bool value in bits)
                {
                    this.bitArray.AddLast(value);
                }
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the list.</param>
        public void AddRange(IEnumerable<bool> collection)
        {
            if (!this.isFixedSize && collection != null)
            {
                foreach (var value in collection)
                {
                    this.bitArray.AddLast(value);
                }
            }
        }

        /// <summary>
        /// Performs the bitwise AND operation between the elements of the current BitList
        /// object and the corresponding elements in the specified BitList. The current BitList
        /// object will be modified to store the result of the bitwise AND operation.
        /// </summary>
        /// <param name="bitsToAnd">The BitList with which to perform the bitwise AND operation.</param>
        public void And(BitList bitsToAnd)
        {
            if (bitsToAnd == null)
            {
                throw new ArgumentNullException(nameof(bitsToAnd));
            }

            if (bitsToAnd.Count != this.bitArray.Count)
            {
                throw new ArgumentException(LocalizationManager.GetMessage("image.tile.differentSize"));
            }

            var result = new LinkedList<bool>();

            int position = 0;
            for (int i = 0; i < this.bitArray.Count; i++)
            {
                var value = this.bitArray.ElementAt(i) & bitsToAnd[i];
                result.AddLast(value);

                position++;
            }

            this.bitArray = result;
        }

        /// <summary>
        /// Removes all elements from the List.
        /// </summary>
        public void Clear()
        {
            this.bitArray.Clear();
        }

        /// <summary>
        /// Concatenates two sequences.
        /// </summary>
        /// <param name="second">The sequence to concatenate to the first sequence.</param>
        /// <returns>A BitList that contains the concatenated elements of the two input sequences.</returns>
        public BitList Concat(BitList second)
        {
            if (this.bitArray == null || second == null)
            {
                throw new ArgumentNullException(LocalizationManager.GetMessage("core.twoArgumentsNullException", "BitList", nameof(second)));
            }

            var result = new BitList();

            foreach (var value in this.bitArray)
            {
                result.Add(value);
            }

            foreach (bool value in second)
            {
                result.Add(value);
            }

            return result;
        }

        /// <summary>
        /// Gets the value of the bit at a specific position in the BitList.
        /// </summary>
        /// <param name="index">The zero-based index of the value to get.</param>
        /// <returns>The value of the bit at position index.</returns>
        public bool Get(int index)
        {
            return this.bitArray.ElementAt(index);
        }

        /// <summary>
        /// Gets the binary representation of the bit at a specific position in the BitList.
        /// </summary>
        /// <param name="index">The zero-based index of the value to get.</param>
        /// <returns>The binary representation of the bit at position index.</returns>
        public string GetBinaryString(int index)
        {
            return this.bitArray.ElementAt(index) ? "1" : "0";
        }

        /// <summary>
        /// Returns an enumerator that iterates through the BitList.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator for the entire BitList.</returns>
        public IEnumerator GetEnumerator()
        {
            return this.bitArray.GetEnumerator();
        }

        /// <summary>
        /// Performs a shift to the left by the number of count.
        /// The current BitList object will be modified to store the result of the left shift operation.
        /// </summary>
        /// <param name="count">Number of shift to do.</param>
        public void Lsl(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (int i = 0; i < count; i++)
            {
                this.bitArray.RemoveFirst();
                this.bitArray.AddLast(false);
            }
        }

        /// <summary>
        /// Inverts all the bit values in the current BitList, so that
        /// elements set to true are changed to false, and elements set to false are changed
        /// to true.
        /// </summary>
        public void Not()
        {
            var result = new LinkedList<bool>();

            int position = 0;
            for (int i = 0; i < this.bitArray.Count; i++)
            {
                result.AddLast(!this.bitArray.ElementAt(i));

                position++;
            }

            this.bitArray = result;
        }

        /// <summary>
        /// Performs the bitwise OR operation between the elements of the current BitList
        /// object and the corresponding elements in the specified BitList. The current BitList
        /// object will be modified to store the result of the bitwise OR operation.
        /// </summary>
        /// <param name="bitsToOr">The BitList with which to perform the bitwise OR operation.</param>
        public void Or(BitList bitsToOr)
        {
            if (bitsToOr == null)
            {
                throw new ArgumentNullException(nameof(bitsToOr));
            }

            if (bitsToOr.Count != this.bitArray.Count)
            {
                throw new ArgumentException(LocalizationManager.GetMessage("image.tile.differentSize"));
            }

            var result = new LinkedList<bool>();

            int position = 0;
            for (int i = 0; i < this.bitArray.Count; i++)
            {
                var value = this.bitArray.ElementAt(i) | bitsToOr[i];
                result.AddLast(value);

                position++;
            }

            this.bitArray = result;
        }

        /// <summary>
        /// Rearrange the bits according to the map.
        /// </summary>
        /// <param name="tilePattern">Informations of the tile.</param>
        /// <returns>The new BitList rearranged.</returns>
        public BitList RearrangeBitsWith2Planes(TilePattern tilePattern)
        {
            if (tilePattern == null)
            {
                throw new ArgumentNullException(LocalizationManager.GetMessage("core.argumentNullException", nameof(tilePattern)));
            }

            if (tilePattern.Map.Count != 2)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectNumberPlan"));
            }

            if (tilePattern.Map[0].Count + tilePattern.Map[1].Count != this.bitArray.Count)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectMapSize"));
            }

            if (tilePattern.Map[0].Count != tilePattern.Map[1].Count)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectPlanSize"));
            }

            int pos = 0;
            var plane1 = new BitList();
            var plane2 = new BitList();
            for (int number = 0; number < tilePattern.Size; number++)
            {
                for (int j = 0; j < tilePattern.Map[0].Count; j++)
                {
                    if (tilePattern.Map[0][j] == number)
                    {
                        plane1.Add(this.bitArray.ElementAt(pos++));
                    }
                }

                for (int j = 0; j < tilePattern.Map[1].Count; j++)
                {
                    if (tilePattern.Map[1][j] == number)
                    {
                        plane2.Add(this.bitArray.ElementAt(pos++));
                    }
                }
            }

            var newBits = new BitList();
            if (tilePattern.Interleave)
            {
                for (int i = 0; i < plane1.Count; i++)
                {
                    newBits.Add(plane2.Get(i));
                    newBits.Add(plane1.Get(i));
                }
            }
            else
            {
                for (int i = 0; i < plane1.Count; i++)
                {
                    newBits.Add(plane1.Get(i));
                    newBits.Add(plane2.Get(i));
                }
            }

            return newBits;
        }

        /// <summary>
        /// Rearrange the bits according to the map.
        /// </summary>
        /// <param name="tilePattern">Informations of the tile.</param>
        /// <returns>The new BitList rearranged.</returns>
        public BitList RearrangeBitsWith2PlanesBack(TilePattern tilePattern)
        {
            if (tilePattern == null)
            {
                throw new ArgumentNullException(LocalizationManager.GetMessage("core.argumentNullException", nameof(tilePattern)));
            }

            if (tilePattern.Map.Count != 2)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectNumberPlan"));
            }

            if (tilePattern.Map[0].Count + tilePattern.Map[1].Count != this.bitArray.Count)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectMapSize"));
            }

            if (tilePattern.Map[0].Count != tilePattern.Map[1].Count)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectPlanSize"));
            }

            var newBits = new BitList();
            var plane1 = new BitList();
            var plane2 = new BitList();
            if (tilePattern.Interleave)
            {
                for (int i = 0; i < this.bitArray.Count; i += 2)
                {
                    plane2.Add(this.bitArray.ElementAt(i));
                    plane1.Add(this.bitArray.ElementAt(i + 1));
                }
            }
            else
            {
                for (int i = 0; i < this.bitArray.Count; i += 2)
                {
                    plane1.Add(this.bitArray.ElementAt(i));
                    plane2.Add(this.bitArray.ElementAt(i + 1));
                }
            }

            int posPlane1 = 0;
            int posPlane2 = 0;
            for (int number = 0; number < tilePattern.Size; number++)
            {
                for (int j = 0; j < tilePattern.Map[0].Count; j++)
                {
                    if (tilePattern.Map[0][j] == number)
                    {
                        newBits.Add(plane1.Get(posPlane1++));
                    }
                }

                for (int j = 0; j < tilePattern.Map[1].Count; j++)
                {
                    if (tilePattern.Map[1][j] == number)
                    {
                        newBits.Add(plane2.Get(posPlane2++));
                    }
                }
            }

            return newBits;
        }

        /// <summary>
        /// Performs a circular shift to the left by the number of count.
        /// The current BitList object will be modified to store the result of the circular left shift operation.
        /// </summary>
        /// <param name="count">Number of shirt to do.</param>
        public void Rol(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (int i = 0; i < count; i++)
            {
                var value = this.bitArray.First();
                this.bitArray.RemoveFirst();
                this.bitArray.AddLast(value);
            }
        }

        /// <summary>
        /// Performs a circular shift to the right by the number of count.
        /// The current BitList object will be modified to store the result of the circular right shift operation.
        /// </summary>
        /// <param name="count">Number of shirt to do.</param>
        public void Ror(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (int i = 0; i < count; i++)
            {
                var value = this.bitArray.Last();
                this.bitArray.RemoveLast();
                this.bitArray.AddFirst(value);
            }
        }

        /// <summary>
        /// Performs a shift to the right by the number of count.
        /// The current BitList object will be modified to store the result of the left shift operation.
        /// </summary>
        /// <param name="count">Number of shift to do.</param>
        public void Rsl(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (int i = 0; i < count; i++)
            {
                this.bitArray.RemoveLast();
                this.bitArray.AddFirst(false);
            }
        }

        /// <summary>
        /// Sets the bit at a specific position in the BitList to the specified value.
        /// </summary>
        /// <param name="index">The zero-based index of the bit to set.</param>
        /// <param name="value">The Boolean value to assign to the bit.</param>
        public void Set(int index, bool value)
        {
            int position = 0;
            for (var currentNode = this.bitArray.First; currentNode != null; currentNode = currentNode.Next)
            {
                if (position == index)
                {
                    this.bitArray.AddBefore(currentNode, value);
                    this.bitArray.Remove(currentNode);
                    break;
                }

                position++;
            }
        }

        /// <summary>
        /// Sets all bits in the BitList to the specified value.
        /// </summary>
        /// <param name="value">The Boolean value to assign to all bits.</param>
        public void SetAll(bool value)
        {
            var result = new LinkedList<bool>();

            for (int i = 0; i < this.bitArray.Count; i++)
            {
                result.AddLast(value);
            }

            this.bitArray = result;
        }

        /// <summary>
        /// Convert all bits into bytes.
        /// </summary>
        /// <returns>An array of bytes.</returns>
        public byte[] ToBytes()
        {
            if (this.bitArray.Count % 8 != 0)
            {
                throw new JHacksonException(LocalizationManager.GetMessage("image.incorrectNumberOfBits"));
            }

            int numberOfBytes = this.bitArray.Count / 8;

            var bytes = new byte[numberOfBytes];
            int position = 0;

            for (int i = 0; i < numberOfBytes; i++)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                {
                    b |= (byte)((this.bitArray.ElementAt(position++) ? 1 : 0) << (8 - 1 - j));
                }

                bytes[i] = b;
            }

            return bytes;
        }

        /// <summary>
        /// Performs the bitwise exclusive OR operation between the elements of the current BitList object against the corresponding elements in the specified BitList.
        /// The current BitList object will be modified to store the result of the bitwise exclusive OR operation.
        /// </summary>
        /// <param name="bitsToXor">The array with which to perform the bitwise exclusive OR operation.</param>
        public void Xor(BitList bitsToXor)
        {
            if (bitsToXor == null)
            {
                throw new ArgumentNullException(nameof(bitsToXor));
            }

            if (bitsToXor.Count != this.bitArray.Count)
            {
                throw new ArgumentException(LocalizationManager.GetMessage("image.tile.differentSize"));
            }

            var result = new LinkedList<bool>();

            int position = 0;
            for (int i = 0; i < this.bitArray.Count; i++)
            {
                var value = this.bitArray.ElementAt(i) ^ bitsToXor[i];
                result.AddLast(value);

                position++;
            }

            this.bitArray = result;
        }
    }
}