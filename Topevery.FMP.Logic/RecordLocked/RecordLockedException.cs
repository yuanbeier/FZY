using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Exception = System.Exception;

namespace Topevery.FMP.Logic
{
    public class RecordLockedException : ArgumentException
    {
        public RecordLockedException() : base()
        {
        }
        
        public RecordLockedException(string message) : base(message)
        {
        }
         
        protected RecordLockedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public RecordLockedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        public RecordLockedException(string message, string paramName) 
            : base(message, paramName)
        {
        }

        public RecordLockedException(string message, string paramName, Exception innerException) 
            : base(message, paramName, innerException)
        {
        }

    }
}
