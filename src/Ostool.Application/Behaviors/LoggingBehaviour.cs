using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
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

            if (response.IsSuccess)
                _logger.LogInformation("{0} Request has been Executed Successfully", name);
            else
                _logger.LogError("{0} Request has FAILED During Execution", name);
            return response;
        }
    }
}