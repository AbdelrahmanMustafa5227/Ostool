using Ostool.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal AvgPrice { get; set; }

        public CarSpecs carSpecs { get; set; } = null!;
        public ICollection<Advertisement> advertisements { get; set; } = new List<Advertisement>();
    }
}