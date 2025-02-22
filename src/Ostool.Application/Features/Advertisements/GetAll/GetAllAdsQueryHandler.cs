using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetAll
{
    public record GetAllAdsQuery(int pageNumber) : IRequest<Result<Paginated<AdvertisementResponse>>>, ICacheable
    {
        public string CacheKey => $"AllAds:{pageNumber}";

        public int DurationInSeconds => 30;
    }

    internal class GetAllAdsQueryHandler : IRequestHandler<GetAllAdsQuery, Result<Paginated<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllAdsQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
        public async Task<Result<Paginated<AdvertisementResponse>>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
        {
            var ads = await _advertisementRepository.GetAll(request.pageNumber);

            var pagedResult = Paginated<AdvertisementResponse>.Create(ads.Items, request.pageNumber, 10, ads.TotalRecords);

            return Result.Success(pagedResult);
        }
    }
}