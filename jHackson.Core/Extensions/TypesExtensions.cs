// <copyright file="TypesExtensions.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Extensions
{
    /// <summary>
    /// Provides a class with methods extension for standard types.
    /// </summary>
    public static class TypesExtensions
    {
        /// <summary>
        /// Test if a bit is set or not.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <param name="posBit">Position of the bit to test.</param>
        /// <returns>Returns True if the bit is set; otherwise False.</returns>
        public static bool IsBitSet(this uint value, int posBit)
        {
            return (value & (1 << posBit)) != 0;
        }

        /// <summary>
        /// Test if a bit is set or not.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <param name="posBit">Position of the bit to test.</param>
        /// <returns>Returns True if the bit is set; otherwise False.</returns>
        public static bool IsBitSet(this byte value, int posBit)
        {
            return (value & (1 << posBit)) != 0;
        }

        /// <summary>
        /// Set a bit.
        /// </summary>
        /// <param name="value">Value to use.</param>
        /// <param name="posBit">Position of the bit to set.</param>
        /// <returns>Returns the value with the bit set.</returns>
        public static uint SetBit(this uint value, int posBit)
        {
            return value | (uint)(1 << posBit);
        }
    }
}