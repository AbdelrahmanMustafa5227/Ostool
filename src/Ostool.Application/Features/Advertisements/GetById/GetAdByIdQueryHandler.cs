using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetById
{
    public record GetAdByIdQuery(Guid Id) : IRequest<Result<AdvertisementDetailedResponse>>;

    internal class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, Result<AdvertisementDetailedResponse>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAdByIdQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Result<AdvertisementDetailedResponse>> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var ad = await _advertisementRepository.GetById(request.Id);

            if (ad == null)
                return Result.Failure<AdvertisementDetailedResponse>(
                    new Error($"No ad found with id {request.Id}",
                    HttpStatusCode.NotFound,
                    "Resource Not Found"));

            return Result.Success(ad);
        }
    }
}