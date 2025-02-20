using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Cars;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddListOfCars
{
    public record AddListOfCarsCommand(List<AddCarCommand> Cars) : IRequest<Result>;

    internal class AddListOfCarsCommandHandler : IRequestHandler<AddListOfCarsCommand, Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;
        private readonly ILogger<AddListOfCarsCommandHandler> _logger;

        public AddListOfCarsCommandHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, ILogger<AddListOfCarsCommandHandler> logger, IPublisher publisher)
        {
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<Result> Handle(AddListOfCarsCommand request, CancellationToken cancellationToken)
        {
            var cars = request.Cars
                .Select(x => x.ToModel())
                .ToList();

            _carRepository.AddRange(cars);
            await _unitOfWork.SaveChangesAsync();

            var distinctBrands = cars
                .Select(car => car.Brand)
                .Distinct();

            foreach (var brand in distinctBrands)
            {
                await _publisher.Publish(new CarCacheInvalidationOnAddOrDeleteEvent(brand));
            }

            _logger.LogInformation("Successfully Added {0} Cars", cars.Count);
            return Result.Success();
        }
    }
}