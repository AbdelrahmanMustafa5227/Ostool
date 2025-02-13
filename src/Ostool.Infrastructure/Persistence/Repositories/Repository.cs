using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public abstract class Repository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        protected Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
    }
}