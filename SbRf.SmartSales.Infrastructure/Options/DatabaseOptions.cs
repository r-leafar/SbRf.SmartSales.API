using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Options
{
    public class DatabaseOptions
    {
        public string? Host { get; set; } = "localhost";
        public string? Port { get; set; } = "5432";
        public string? Database { get; set; } = "postgres";
        public string? Username { get; set; } = "postgres";
        public string? Password { get; set; } = "postgres";
        public string? URL { get; set; } 
    }

}
