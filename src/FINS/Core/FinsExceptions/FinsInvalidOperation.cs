using System;

namespace FINS.Core.FinsExceptions
{
    public class FinsInvalidOperation : Exception
    {
        public FinsInvalidOperation()
        {
        }

        public FinsInvalidOperation(string message) : base(message)
        {
        }

        public FinsInvalidOperation(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
