using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.GetByBrand
{
    public record GetByBrandCommand(string Brand) : IRequest<Result<List<GetByBrandResponse>>>, ICacheable
    {
        public string CacheKey => $"Cars:{Brand}";
    }

    public record GetByBrandResponse(string Brand, string Model, decimal AvgPrice);

    internal class GetByBrandCommandHandler : IRequestHandler<GetByBrandCommand, Result<List<GetByBrandResponse>>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ITestableLogger<GetByBrandCommandHandler> _logger;

        public GetByBrandCommandHandler(ICarRepository carRepository, ITestableLogger<GetByBrandCommandHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<Result<List<GetByBrandResponse>>> Handle(GetByBrandCommand request, CancellationToken cancellationToken)
        {
            var Cars = await _carRepository.GetAllByBrand(request.Brand);

            if (Cars == null)
                return new List<GetByBrandResponse>();

            var responseDto = new List<GetByBrandResponse>();

            foreach (var Car in Cars)
            {
                responseDto.Add(Car.ToDto());
            }

            return responseDto;
        }
    }
}