using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Property
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public decimal MonthlyPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsAvailable { get; set; }

        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}
