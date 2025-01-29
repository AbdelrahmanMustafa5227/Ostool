using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.UpdateCar
{
    public record UpdateCarCommand(Guid Id, string Brand, string Model, int Year, decimal AvgPrice) : IRequest<Result>;

    internal class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByModelName(request.Model);

            if (car is null)
            {
                return Result.Failure(new Error("Provided Car Model is not found", HttpStatusCode.NotFound, "Resource Not Found"));
            }

            _carRepository.Update(car.ApplyChanges(request));
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}