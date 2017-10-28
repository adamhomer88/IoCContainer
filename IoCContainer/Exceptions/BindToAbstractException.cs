using System;
using System.Runtime.Serialization;

namespace IoCContainer.Exceptions
{
    public class BindToAbstractException: Exception
    {
        public BindToAbstractException()
        {

        }

        public BindToAbstractException(string message)
            :base(message)
        {

        }

        public BindToAbstractException(string message, Exception innerException)
            :base(message, innerException)
        {

        }

        protected BindToAbstractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
