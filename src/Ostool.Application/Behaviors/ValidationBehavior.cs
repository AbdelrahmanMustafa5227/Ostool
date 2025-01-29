using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Exceptions;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal partial class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                await next();
            }

            var context = new ValidationContext<TRequest>(request);


            var validationErrors = _validators
                .Select(x => x.Validate(context))
                .Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
                .ToList();


            if (validationErrors.Any())
            {
                _logger.LogError("Validation Error Has Occurred on Request {0}", request.GetType().Name);
                throw new ValidationFailureException(validationFailures: validationErrors);
            }

            return await next();

        }
    }
}