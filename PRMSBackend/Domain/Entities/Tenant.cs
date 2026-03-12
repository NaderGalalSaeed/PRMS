using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tenant
    {
        public long Id { get; set; }

        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public long? NationalId { get; set; }

        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

        public virtual Nationality? National { get; set; }
    }
}
