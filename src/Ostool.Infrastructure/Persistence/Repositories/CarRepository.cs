using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Car car)
        {
            _dbContext.Cars.Add(car);
        }

        public void Delete(Car car)
        {
            _dbContext.Cars.Remove(car);
        }

        public async Task<List<Car>> GetAll()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        public async Task<List<Car>> GetAllByBrand(string brand)
        {
            return await _dbContext.Cars.Where(x => x.Brand == brand).ToListAsync();
        }

        public async Task<Car?> GetByModelName(string modelName)
        {
            return await _dbContext.Cars.FirstOrDefaultAsync(x => x.Model == modelName);
        }

        public void Update(Car car)
        {
            _dbContext.Cars.Update(car);
        }
    }
}
