using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Favourites
    {
        public Guid Id { get; set; }

        public Guid VisitorId { get; set; }
        public Visitor Visitor { get; set; } = null!;

        public Guid AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; } = null!;
    }
}