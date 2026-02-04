using SbRf.SmartSales.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SbRf.SmartSales.Core.Share.Ensure
{
    public static partial class Ensure
    {
        public static void HasItens<T>(ICollection<T> value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null)
            {
                throw new DomainException(paramName ?? nameof(value), "Value cannot be null.");
            }
            if (value.Count == 0)
            {
                throw new DomainException(paramName ?? nameof(value), "Collection cannot be empty.");
            }
        }

    }
}