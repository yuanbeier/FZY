using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Topevery.FMP.ObjectModel.Exceptions
{
    [Serializable]
    public class ExecuteResultException : ApplicationException
    {
        public ExecuteResultException()
            : base()
        {

        }


        public ExecuteResultException(string message)
            : base(message)
        {
        }

        protected ExecuteResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ExecuteResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }



    }
}
