using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lease
    {
        public long Id { get; set; }

        public long PropertyId { get; set; }

        public long TenantId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal MonthlyPrice { get; set; }

        public virtual Property Property { get; set; } = null!;

        public virtual Tenant Tenant { get; set; } = null!;
    }
}
