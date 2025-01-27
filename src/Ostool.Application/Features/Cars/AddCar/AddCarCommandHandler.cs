using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddCar
{
    public record AddCarCommand(string Brand , string Model , int Year , decimal AvgPrice) : IRequest<Result>;

    internal class AddCarCommandHandler : IRequestHandler<AddCarCommand,Result>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _carRepository.Add(new Car { Id = Guid.NewGuid() , Model = request.Model ,
                    Brand = request.Brand , AvgPrice = request.AvgPrice , Year = request.Year} );
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(ex.Message +"\n" + ex.InnerException?.Message));   
            }

        }
    }
}
