using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.CarSpecs.AddCarDetails
{
    public record AddCarDetailsCommand(Guid CarId, CarBodyStyle BodyStyle, double GroundClearance,
        EngineType EngineType, int Displacement, int numOfCylinders, int HorsePower,
        TransmissionType TransmissionType, int numOfGears, double TopSpeed, double ZeroToSixty,
        bool HasSumRoof, int SeatingCapacity) : IRequest<Result>;

    public class AddCarDetailsCommandHandler : IRequestHandler<AddCarDetailsCommand, Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarSpecsRepository _carSpecsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCarDetailsCommandHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, ICarSpecsRepository carSpecsRepository)
        {
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _carSpecsRepository = carSpecsRepository;
        }


        public async Task<Result> Handle(AddCarDetailsCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.CarId);
            if (car == null)
                return Result.Failure(new Error("No Car With Supplied Id", HttpStatusCode.NotFound, "Resource Not Found"));

            _carSpecsRepository.Add(request.ToModel());
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}