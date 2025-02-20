using Microsoft.Extensions.Caching.Memory;
using Ostool.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Caching.Vendors
{
    internal class VendorsCacheInvalidationHandler : INotificationHandler<VendorsCacheInvalidationEvent>
    {
        private readonly CachingService _cache;

        public VendorsCacheInvalidationHandler(CachingService cache)
        {
            _cache = cache;
        }
        public Task Handle(VendorsCacheInvalidationEvent notification, CancellationToken cancellationToken)
        {
            _cache.Remove(x => x.StartsWith("All-Vendors"));
            return Task.CompletedTask;
        }
    }
}