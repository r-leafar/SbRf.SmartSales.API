using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Exceptions
{
    public class InfraestructureException : Exception
    {
        public InfraestructureException(string message) : base(message) { }

        public InfraestructureException(string message, string paramName) : this(message + $" (Parameter: {paramName})") { }
        public InfraestructureException(string message, Exception innerException)
      : base(message, innerException) { }
    }
}
