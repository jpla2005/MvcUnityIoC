using System;
using System.Runtime.Serialization;

namespace MvcUnityIoC
{
    public class IoCException : ApplicationException
    {
        public IoCException()
        {
        }

        public IoCException(string message)
            : base(message)
        {
        }

        public IoCException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public IoCException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}