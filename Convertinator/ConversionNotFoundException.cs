using System;
using System.Runtime.Serialization;

namespace Convertinator
{
    public class ConversionNotFoundException : Exception
    {
        public ConversionNotFoundException()
        {
        }

        public ConversionNotFoundException(string message) : base(message)
        {
        }

        public ConversionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConversionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}