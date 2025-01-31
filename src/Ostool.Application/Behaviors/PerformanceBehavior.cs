using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var response = await next();
            stopWatch.Stop();

            _logger.LogInformation("{0} Request Took {1} Milliseconds \n -------", request.GetType().Name, stopWatch.ElapsedMilliseconds);
            return response;
        }
    }
}