using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
        public ApplicationException(string message, string paramName) : this(message + $" (Parameter: {paramName})") { }
        public ApplicationException(string message, Exception innerException)
      : base(message, innerException) { }
    }
}
