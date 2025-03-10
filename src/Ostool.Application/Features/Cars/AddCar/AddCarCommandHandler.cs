﻿using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Cars;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddCar
{
    public record AddCarCommand(string Brand, string Model, decimal AvgPrice) : IRequest<Result<AddCarResponse>>;

    public record AddCarResponse(Guid Id, string Brand, string Model, decimal AvgPrice);

    internal class AddCarCommandHandler : IRequestHandler<AddCarCommand, Result<AddCarResponse>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestableLogger<AddCarCommandHandler> _logger;
        private readonly IPublisher _publisher;

        public AddCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork, ITestableLogger<AddCarCommandHandler> logger, IPublisher publisher)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<Result<AddCarResponse>> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var carFromDb = await _carRepository.GetByModelName(request.Model);
            if (carFromDb is not null)
            {
                _logger.LogError("A Conflict Error has occurred with message \"This Car Brand Already Exists\"");
                return Result.Failure<AddCarResponse>(new Error("This Car Brand Already Exists", HttpStatusCode.Conflict, "Conflict"));
            }

            var car = request.ToModel();
            _carRepository.Add(car);
            await _unitOfWork.SaveChangesAsync();
            await _publisher.Publish(new CarCacheInvalidationOnAddOrDeleteEvent(request.Brand));
            var response = new AddCarResponse(car.Id, car.Brand, car.Model, car.AvgPrice);
            _logger.LogInformation("Successfully Added {0}", request.Brand + " " + request.Model);
            return Result.Success(response);
        }
    }
}