using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Vendor
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string ContactNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public ICollection<Advertisement> Advertisements { get; private set; } = new List<Advertisement>();
    }
}
