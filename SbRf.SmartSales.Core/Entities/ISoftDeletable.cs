using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entities
{
    public interface ISoftDeletable
    {
        DateTime? DeletedAt { get; }
    }
}
