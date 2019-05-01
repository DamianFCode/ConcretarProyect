using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public class NoRecordFoundException : DataManagementException
    {
        public NoRecordFoundException() : base("No records has been found")
        {
        }
        public NoRecordFoundException(string message) : base(message)
        {
        }
        public NoRecordFoundException(string message, Exception inner) : base(message, inner)
        {
        }
        protected NoRecordFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
