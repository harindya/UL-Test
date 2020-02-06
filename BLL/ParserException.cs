using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BLL
{   
    [Serializable]
    public class ParserException : ApplicationException
    {
        public ParserException()
        { }

        public ParserException(string message) : base(message)
        { }

        public ParserException(string message, Exception inner) : base(message, inner)
        { }

        protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
