using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public class GetRecordException : DataManagementException
    {
        public GetRecordException() : base("An error occurred while trying to obtain the object")
        {
        }
        public GetRecordException(string message) : base(message)
        {
        }
        public GetRecordException(string message, Exception inner) : base(message, inner)
        {
        }
        protected GetRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
