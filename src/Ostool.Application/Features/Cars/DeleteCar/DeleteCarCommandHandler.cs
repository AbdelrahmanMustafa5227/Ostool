using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Cars;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.DeleteCar
{
    public record DeleteCarCommand(Guid CarId) : IRequest<Result>;

    internal class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;
        private readonly ILogger<DeleteCarCommand> _logger;

        public DeleteCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork, ILogger<DeleteCarCommand> logger, IPublisher publisher)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.CarId);

            if (car == null)
                return Result.Failure(new Error(
                    "Could not find a car with the supplied ID",
                    HttpStatusCode.NotFound, "Resource Not Found")
                    );

            _carRepository.Delete(car);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new CarCacheInvalidationOnAddOrDeleteEvent(car.Brand));
            _logger.LogInformation("Successfully Deleted {0}", car.Brand + " " + car.Model);
            return Result.Success();
        }
    }
}