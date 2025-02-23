using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Visitors.GetSavedAds
{
    public record GetSavedAdsQuery(Guid VisitorId) : IRequest<Result<List<AdvertisementResponse>>>;

    internal class GetSavedAdsQueryHandler : IRequestHandler<GetSavedAdsQuery, Result<List<AdvertisementResponse>>>
    {
        private readonly IFavouritesRepository _favouritesRepository;

        public GetSavedAdsQueryHandler(IFavouritesRepository favouritesRepository)
        {
            _favouritesRepository = favouritesRepository;
        }
        public async Task<Result<List<AdvertisementResponse>>> Handle(GetSavedAdsQuery request, CancellationToken cancellationToken)
        {
            var ads = await _favouritesRepository.GetAllSavedAdsByVisitorId(request.VisitorId);

            return Result.Success(ads);
        }
    }
}
