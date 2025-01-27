using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal AvgPrice { get; set; }

        public CarSpecs carSpecs { get; set; } = new();
        public ICollection<Advertisement> advertisements { get; set; } = new List<Advertisement>();
    }
}