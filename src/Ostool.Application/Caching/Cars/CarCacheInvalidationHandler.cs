using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Caching.Cars
{
    internal class CarCacheInvalidationHandler : INotificationHandler<CarCacheInvalidationOnAddOrDeleteEvent>
    {
        private readonly IMemoryCache _cache;

        public CarCacheInvalidationHandler(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task Handle(CarCacheInvalidationOnAddOrDeleteEvent notification, CancellationToken cancellationToken)
        {
            _cache.Remove($"Cars:{notification.Brand}");
            await Task.CompletedTask;
        }
    }
}