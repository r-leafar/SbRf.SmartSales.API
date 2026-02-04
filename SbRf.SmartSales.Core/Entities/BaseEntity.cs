using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; private set; } = default!;

        protected void SetId(TId id)
        {
            Id = id;
        }

    }
}
