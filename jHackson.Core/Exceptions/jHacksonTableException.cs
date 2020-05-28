// <copyright file="jHacksonTableException.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Exceptions
{
    using System;

    public class JHacksonTableException : JHacksonException
    {
        public JHacksonTableException()
        {
        }

        public JHacksonTableException(string message)
            : base(message)
        {
        }

        public JHacksonTableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}