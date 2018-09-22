using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Exceptions
{
    public class EntitiesException : Exception
    {
        public enum ExceptionStatusCode
        {
            NotFound,
            InvalidData
        }
        public EntitiesException() { }

        public EntitiesException(string message, ExceptionStatusCode statusCode)
        : base(message) { }

        public EntitiesException(string message, Exception inner)
        : base(message, inner) { }
    }
}
