using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface ICarSpecsRepository
    {
        void Add(CarSpecs specs);
        void Update(CarSpecs specs);
        Task<CarSpecs?> GetById(Guid carId);
    }
}