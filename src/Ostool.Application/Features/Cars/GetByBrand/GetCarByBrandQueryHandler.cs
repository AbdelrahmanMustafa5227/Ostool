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
    public record GetCarByBrandQuery(string Brand, int pageNumber) : IRequest<Result<Paginated<GetCarByBrandResponse>>>, ICacheable
    {
        public string CacheKey => $"Cars:{Brand}:{pageNumber}";
        public int DurationInSeconds => 10 * 60;
    }

    public record GetCarByBrandResponse(string Brand, string Model, decimal AvgPrice);

    internal class GetCarByBrandQueryHandler : IRequestHandler<GetCarByBrandQuery, Result<Paginated<GetCarByBrandResponse>>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ITestableLogger<GetCarByBrandQueryHandler> _logger;

        public GetCarByBrandQueryHandler(ICarRepository carRepository, ITestableLogger<GetCarByBrandQueryHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<Result<Paginated<GetCarByBrandResponse>>> Handle(GetCarByBrandQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _carRepository.GetAllByBrand(request.Brand, request.pageNumber);

            var responseDto = queryResult.Items.Select(x => x.ToDto()).ToList();

            var pagedResult = Paginated<GetCarByBrandResponse>.Create(responseDto, request.pageNumber, 10, queryResult.TotalRecords);

            return pagedResult;
        }
    }
}