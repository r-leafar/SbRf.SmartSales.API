using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entities
{
    public interface IHistory
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
