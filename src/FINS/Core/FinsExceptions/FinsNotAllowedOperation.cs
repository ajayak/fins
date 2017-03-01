using System;

namespace FINS.Core.FinsExceptions
{
    public class FinsNotAllowedOperation : Exception
    {
        public FinsNotAllowedOperation()
        {
        }

        public FinsNotAllowedOperation(string message) : base(message)
        {
        }

        public FinsNotAllowedOperation(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
