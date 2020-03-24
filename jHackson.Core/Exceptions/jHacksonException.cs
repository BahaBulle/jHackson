using System;

namespace jHackson.Core.Exceptions
{
    public class JHacksonException : Exception
    {
        public JHacksonException()
        {
        }

        public JHacksonException(string message)
            : base(message)
        {
        }

        public JHacksonException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}