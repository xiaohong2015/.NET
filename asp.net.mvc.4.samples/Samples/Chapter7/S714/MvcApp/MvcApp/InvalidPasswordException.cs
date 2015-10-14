using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp
{
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
        protected InvalidPasswordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
