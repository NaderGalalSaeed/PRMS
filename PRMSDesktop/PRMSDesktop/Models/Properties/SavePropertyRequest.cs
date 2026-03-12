using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Models.Properties
{
    public class SavePropertyRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal MonthlyPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}
