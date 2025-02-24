using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IWatchlistRepository
    {
        void Add(WatchList watchList);
        void Delete(WatchList watchList);
        Task<List<string>> GetObserversEmails(Guid carID);
    }
}