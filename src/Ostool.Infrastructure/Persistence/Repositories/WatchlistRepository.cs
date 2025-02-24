using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    internal class WatchlistRepository : IWatchlistRepository
    {
        private readonly AppDbContext _appDbContext;

        public WatchlistRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(WatchList watchList)
        {
            _appDbContext.WatchList.Add(watchList);
        }

        public void Delete(WatchList watchList)
        {
            _appDbContext.WatchList.Remove(watchList);
        }

        public async Task<List<string>> GetObserversEmails(Guid carID)
        {
            return await _appDbContext.WatchList
                .Where(w => w.CarId == carID)
                .Select(w => w.Visitor.Email)
                .ToListAsync();
        }
    }
}