using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public abstract class DataManagementException : Exception
    {
        protected DataManagementException()
        {
        }
        protected DataManagementException(string message) : base(message)
        {
        }
        protected DataManagementException(string message, Exception inner) : base(message, inner)
        {
        }
        protected DataManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
