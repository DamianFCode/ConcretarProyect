using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public class CreateRecordException : DataManagementException
    {
        public CreateRecordException() : base("An error occurred while trying to create the object")
        {
        }
        public CreateRecordException(string message) : base(message)
        {
        }
        public CreateRecordException(string message, Exception inner) : base(message, inner)
        {
        }
        protected CreateRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
