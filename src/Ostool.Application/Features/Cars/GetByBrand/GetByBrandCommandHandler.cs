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
    public record GetByBrandCommand(string Brand, int pageNumber) : IRequest<Result<Paginated<GetByBrandResponse>>>, ICacheable
    {
        public string CacheKey => $"Cars:{Brand}:{pageNumber}";
        public int DurationInSeconds => 10 * 60;
    }

    public record GetByBrandResponse(string Brand, string Model, decimal AvgPrice);

    internal class GetByBrandCommandHandler : IRequestHandler<GetByBrandCommand, Result<Paginated<GetByBrandResponse>>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ITestableLogger<GetByBrandCommandHandler> _logger;

        public GetByBrandCommandHandler(ICarRepository carRepository, ITestableLogger<GetByBrandCommandHandler> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<Result<Paginated<GetByBrandResponse>>> Handle(GetByBrandCommand request, CancellationToken cancellationToken)
        {
            var queryResult = await _carRepository.GetAllByBrand(request.Brand, request.pageNumber);

            var responseDto = queryResult.Items.Select(x => x.ToDto()).ToList();

            var pagedResult = Paginated<GetByBrandResponse>.Create(responseDto, request.pageNumber, 10, queryResult.TotalRecords);

            return pagedResult;
        }
    }
}