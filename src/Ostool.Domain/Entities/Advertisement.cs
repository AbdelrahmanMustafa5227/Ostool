using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Advertisement
    {
        public Guid Id { get; set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public DateTime PostedDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }

        public Vendor Vendor { get; set; } = new();
        public Guid VendorId { get; set; }

        public Car Car { get; set; } = new();
        public Guid CarId { get; set; }
    }
}
