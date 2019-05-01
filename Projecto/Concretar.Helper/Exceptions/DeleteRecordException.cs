using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public class DeleteRecordException : DataManagementException
    {
        public DeleteRecordException() : base("An error occurred while attempting to delete the object")
        {
        }
        public DeleteRecordException(string message) : base(message)
        {
        }
        public DeleteRecordException(string message, Exception inner) : base(message, inner)
        {
        }
        protected DeleteRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
