// <copyright file="TableElementsExtensions.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.TableElements.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class TableElementsExtensions
    {
        public static IEnumerable<string> SplitByLength(this string value, int maxLength)
        {
            if (value == null)
            {
                yield break;
            }

            for (int index = 0; index < value.Length; index += maxLength)
            {
                yield return value.Substring(index, Math.Min(maxLength, value.Length - index));
            }
        }
    }
}