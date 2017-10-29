using System;
using System.Runtime.Serialization;

namespace IoCContainer.Exceptions
{
    public class DuplicateBindingException : Exception
    {
        public DuplicateBindingException()
        {

        }

        public DuplicateBindingException(string message)
            :base(message)
        {

        }

        public DuplicateBindingException(string message, Exception innerException)
            :base(message, innerException)
        {

        }

        protected DuplicateBindingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
