using System;
using System.Runtime.Serialization;

namespace IoCContainer.Exceptions
{
    public class DuplicateBindingKeyException : Exception
    {
        public DuplicateBindingKeyException()
        {

        }

        public DuplicateBindingKeyException(string message)
            :base(message)
        {

        }

        public DuplicateBindingKeyException(string message, Exception innerException)
            :base(message, innerException)
        {

        }

        protected DuplicateBindingKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
