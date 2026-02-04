using SbRf.SmartSales.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SbRf.SmartSales.Core.Share.Ensure
{
    public static partial class Ensure
    {
        public static void IsNonNegative(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value < 0)
            {
                throw new DomainException("Value must be positve.", paramName ?? nameof(value));
            }
        }

        public static void IsGreaterThanZero(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
        {     
            if (value <= 0)
            {
                throw new DomainException("Value must be greater than zero.", paramName ?? nameof(value));
            }
        }
    }
}
