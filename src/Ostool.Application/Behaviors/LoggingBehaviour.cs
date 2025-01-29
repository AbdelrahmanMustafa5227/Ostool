using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            _logger.LogInformation("Ececuting {0} Request", name);
            var response = await next();
            _logger.LogInformation("{0} Request has been Executed Successfully", name);
            return response;
        }
    }
}