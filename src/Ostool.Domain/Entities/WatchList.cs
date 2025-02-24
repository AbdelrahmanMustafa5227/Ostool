using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class WatchList
    {
        public Car Car { get; set; } = null!;
        public Guid CarId { get; set; }

        public Visitor Visitor { get; set; } = null!;
        public Guid VisitorId { get; set; }
    }
}