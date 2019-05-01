using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Concretar.Helper.Exceptions
{
    [Serializable]
    public class UpdateRecordException : DataManagementException
    {
        public UpdateRecordException() : base("An error occurred while trying to update the object")
        {
        }
        public UpdateRecordException(string message) : base(message)
        {
        }
        public UpdateRecordException(string message, Exception inner) : base(message, inner)
        {
        }
        protected UpdateRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
