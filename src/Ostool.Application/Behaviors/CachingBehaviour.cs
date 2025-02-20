using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Helpers;
using Ostool.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheable
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IMemoryCache _cache;
        private readonly CachingService _cachingService;

        public CachingBehaviour(ILogger<TRequest> logger, IMemoryCache cache, CachingService cachingService)
        {
            _logger = logger;
            _cache = cache;
            _cachingService = cachingService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_cachingService.TryGetValue(request.CacheKey, out TResponse? cachedResponse))
            {
                _logger.LogInformation("Cache Hit !");
                return cachedResponse!;
            }

            _logger.LogInformation("Cache Miss !");
            var response = await next();

            _cachingService.Set(request.CacheKey, response, TimeSpan.FromSeconds(request.DurationInSeconds));
            return response;
        }
    }
}