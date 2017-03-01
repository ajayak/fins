using System;

namespace FINS.Core.FinsExceptions
{
    public class FinsNotFoundException : Exception
    {
        public FinsNotFoundException()
        {
        }

        public FinsNotFoundException(string message) : base(message)
        {
        }

        public FinsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
