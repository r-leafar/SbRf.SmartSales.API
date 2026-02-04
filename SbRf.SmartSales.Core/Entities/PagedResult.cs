using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entities
{
    public class PagedResult<TEntity>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<TEntity> Items { get; set; } = new();
    }
}
