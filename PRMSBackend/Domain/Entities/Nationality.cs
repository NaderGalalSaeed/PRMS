using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nationality
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    }
}
