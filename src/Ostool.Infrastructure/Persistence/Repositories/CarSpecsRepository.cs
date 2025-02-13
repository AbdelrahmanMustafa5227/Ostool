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
    public class CarSpecsRepository : ICarSpecsRepository
    {
        private readonly AppDbContext _dbContext;

        public CarSpecsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(CarSpecs specs)
        {
            _dbContext.CarSpecs.Add(specs);
        }

        public async Task<CarSpecs?> GetById(Guid carId)
        {
            return await _dbContext.CarSpecs.FirstOrDefaultAsync(x => x.CarId == carId);
        }

        public void Update(CarSpecs specs)
        {
            _dbContext.CarSpecs.Update(specs);
        }
    }
}