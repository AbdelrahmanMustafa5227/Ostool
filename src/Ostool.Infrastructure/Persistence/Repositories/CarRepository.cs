using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Car car)
        {
            _dbContext.Cars.Add(car);
        }

        public void AddRange(List<Car> cars)
        {
            _dbContext.Cars.AddRange(cars);
        }

        public void Delete(Car car)
        {
            _dbContext.Cars.Remove(car);
        }

        public async Task<QueryResult<Car>> GetAll(int pageNumber)
        {
            var query = _dbContext.Cars
                .AsNoTracking();

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderBy(x => x.Model)
                .Skip((pageNumber - 1) * 5)
                .Take(5)
                .ToListAsync();

            return new QueryResult<Car>(list, totalRecords);
        }

        public async Task<QueryResult<Car>> GetAllByBrand(string brand, int pageNumber)
        {
            var query = _dbContext.Cars
                .AsNoTracking()
                .Where(x => x.Brand == brand);

            var TotalRecords = await query.CountAsync();

            var list = await query
                .OrderBy(x => x.Model)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .ToListAsync();

            return new QueryResult<Car>(list, TotalRecords);
        }

        public async Task<Car?> GetById(Guid carId)
        {
            return await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == carId);
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