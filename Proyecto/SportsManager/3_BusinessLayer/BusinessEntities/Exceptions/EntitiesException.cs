using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Exceptions
{
    public class EntitiesException : Exception
    {
        
        public ExceptionStatusCode StatusCode { get; private set; }

        public EntitiesException()
        {
            this.StatusCode = ExceptionStatusCode.Undefined;
        }

        public EntitiesException(string message, ExceptionStatusCode statusCode)
        : base(message)
        {
            this.StatusCode = statusCode;
        }

        public EntitiesException(string message, ExceptionStatusCode statusCode, Exception inner)
        : base(message, inner)
        {
            this.StatusCode = statusCode;
        }
    }
    public enum ExceptionStatusCode
    {
        Undefined = 500,
        NotFound = 404,
        InvalidData = 400,
        Conflict = 409,
        NotModified = 304
    }
}
