using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    internal class OutboxRepository : IOutboxRepository
    {
        private readonly AppDbContext _appDbContext;

        public OutboxRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(OutboxMessage message)
        {
            _appDbContext.outboxMessages.Add(message);
        }

        public async Task<List<OutboxMessage>> GetUnprocessedMessages()
        {
            return await _appDbContext.outboxMessages.Where(x => x.Processed == false).ToListAsync();
        }

    }
}