using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Caching.Vendors
{
    internal class VendorsCacheInvalidationHandler : INotificationHandler<VendorsCacheInvalidationEvent>
    {
        private readonly IMemoryCache _cache;

        public VendorsCacheInvalidationHandler(IMemoryCache cache)
        {
            _cache = cache;
        }
        public Task Handle(VendorsCacheInvalidationEvent notification, CancellationToken cancellationToken)
        {
            _cache.Remove("All-Vendors");
            return Task.CompletedTask;
        }
    }
}