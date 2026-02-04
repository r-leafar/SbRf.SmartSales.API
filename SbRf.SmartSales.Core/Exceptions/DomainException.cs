using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        public DomainException(string message, string paramName) : this(message + $" (Parameter: {paramName})")
        {
        }
        public DomainException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
