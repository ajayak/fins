using System;

namespace FINS.Core.FinsExceptions
{
    public class FinsInvalidDataException : Exception
    {
        public FinsInvalidDataException()
        {
        }

        public FinsInvalidDataException(string message) : base(message)
        {
        }

        public FinsInvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
