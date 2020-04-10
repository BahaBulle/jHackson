using System;

namespace jHackson.Core.Exceptions
{
    public class jHacksonTableException : JHacksonException
    {
        public jHacksonTableException()
        {
        }

        public jHacksonTableException(string message) : base(message)
        {
        }

        public jHacksonTableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}