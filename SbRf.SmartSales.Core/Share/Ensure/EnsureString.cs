using SbRf.SmartSales.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SbRf.SmartSales.Core.Share.Ensure
{
    public static partial class Ensure
    {

        public static void NotNullOrWhiteSpace(string? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            NotNull(value, paramName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Value cannot be empty", paramName ?? nameof(value));
            }
        }
        public static void NotNull(string? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null)
            {
                throw new DomainException(paramName ?? nameof(value), "Value cannot be null.");
            }
        }
        public static void NotNull<T>(T? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null)
            {
                throw new DomainException(paramName ?? nameof(value), "Value cannot be null.");
            }
        }
    }
}