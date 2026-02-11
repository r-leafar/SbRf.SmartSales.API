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

        public static void NoDuplicateValues<T, TKey>( ICollection<T> list,Func<T, TKey> selector,   [CallerArgumentExpression(nameof(list))] string? paramList = null, 
                [CallerArgumentExpression(nameof(selector))] string? paramName = null)
        {
            if (list is null)
                throw new DomainException($"The list \"{ list }\" is null");

            if (selector is null)
                throw new DomainException($"The selector \"{selector}\" is nul");

            bool hasDuplicates = list
                .GroupBy(selector)
                .Any(g => g.Count() > 1);

            if (hasDuplicates)
                throw new DomainException(
                    $"Duplicate values found for {paramName}.");

        }


    }
}