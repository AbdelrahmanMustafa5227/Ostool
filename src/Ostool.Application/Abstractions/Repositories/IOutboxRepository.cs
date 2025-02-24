﻿using Ostool.Application.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        void Add(OutboxMessage message);
        Task<List<OutboxMessage>> GetUnprocessedMessages();
    }
}