using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface ICarRepository
    {
        void Add(Car car);
        void AddRange(List<Car> cars);
        void Update(Car car);
        void Delete(Car car);
        Task<List<Car>> GetAll();
        Task<List<Car>> GetAllByBrand(string brand);
        Task<Car?> GetByModelName(string modelName);
        Task<Car?> GetById(Guid carId);

    }
}