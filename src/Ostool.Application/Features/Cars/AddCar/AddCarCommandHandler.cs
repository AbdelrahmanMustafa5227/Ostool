using MediatR;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddCar
{
    public record AddCarCommand(string Brand, string Model, int Year, decimal AvgPrice) : IRequest<Result>;

    internal class AddCarCommandHandler : IRequestHandler<AddCarCommand, Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddCarCommandHandler> _logger;

        public AddCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork, ILogger<AddCarCommandHandler> logger)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            _carRepository.Add(request.ToModel());
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Successfully Added {0}", request.Brand + " " + request.Model);
            return Result.Success();
        }
    }
}