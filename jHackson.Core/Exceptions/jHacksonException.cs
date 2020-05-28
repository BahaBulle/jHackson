// <copyright file="JHacksonException.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Exceptions
{
    using System;

    public class JHacksonException : Exception
    {
        public JHacksonException()
        {
        }

        public JHacksonException(string message)
            : base(message)
        {
        }

        public JHacksonException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}