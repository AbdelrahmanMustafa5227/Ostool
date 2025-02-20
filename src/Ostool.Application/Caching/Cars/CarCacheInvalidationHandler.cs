using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Ostool.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Caching.Cars
{
    internal class CarCacheInvalidationHandler : INotificationHandler<CarCacheInvalidationOnAddOrDeleteEvent>
    {
        private readonly CachingService _cache;

        public CarCacheInvalidationHandler(CachingService cache)
        {
            _cache = cache;
        }

        public async Task Handle(CarCacheInvalidationOnAddOrDeleteEvent notification, CancellationToken cancellationToken)
        {
            _cache.Remove(x => x.StartsWith($"Cars:{notification.Brand}"));
            await Task.CompletedTask;
        }
    }
}