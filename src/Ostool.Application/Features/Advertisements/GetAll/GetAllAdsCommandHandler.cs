using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetAll
{
    public record GetAllAdsCommand : IRequest<Result<List<AdvertisementResponse>>>;

    internal class GetAllAdsCommandHandler : IRequestHandler<GetAllAdsCommand, Result<List<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllAdsCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
        public async Task<Result<List<AdvertisementResponse>>> Handle(GetAllAdsCommand request, CancellationToken cancellationToken)
        {
            var ads = await _advertisementRepository.GetAll();
            return Result.Success(ads);
        }
    }
}