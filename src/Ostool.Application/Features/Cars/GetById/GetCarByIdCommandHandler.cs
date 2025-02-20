using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.GetById
{
    public record GetCarByIdCommand(Guid Id) : IRequest<Result<GetCarByIdResponse>>;

    public record GetCarByIdResponse(Guid Id, string Brand, string Model, decimal AvgPrice);

    internal class GetCarByIdCommandHandler : IRequestHandler<GetCarByIdCommand, Result<GetCarByIdResponse>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<Result<GetCarByIdResponse>> Handle(GetCarByIdCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.Id);

            if (car is null)
            {
                return Result.Failure<GetCarByIdResponse>(new Error("Car Not Found", HttpStatusCode.NotFound, "NotFound"));
            }

            var response = new GetCarByIdResponse(car.Id, car.Brand, car.Model, car.AvgPrice);
            return response;
        }
    }
}