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
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public Vendor Vendor { get; set; } = null!;
        public Guid VendorId { get; set; }

        public Car Car { get; set; } = null!;
        public Guid CarId { get; set; }
    }
}