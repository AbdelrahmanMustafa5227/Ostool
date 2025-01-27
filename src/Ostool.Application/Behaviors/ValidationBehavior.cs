using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Ostool.Application.Features.Cars.AddCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
            {
                await next();
            }

            var validationErrors = _validators
                .Select(x => x.Validate(request))
                .Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors);

            if (!validationErrors.Any())
            {
                throw new ValidationException(validationErrors);
            }

            return await next();

        }
    }
}
