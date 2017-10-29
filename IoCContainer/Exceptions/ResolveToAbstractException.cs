using System;
using System.Runtime.Serialization;

namespace IoCContainer.Exceptions
{
    public class ResolveToAbstractException : Exception
    {
        public ResolveToAbstractException()
        {

        }

        public ResolveToAbstractException(string message)
            :base(message)
        {

        }

        public ResolveToAbstractException(string message, Exception innerException)
            :base(message, innerException)
        {

        }

        protected ResolveToAbstractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
