using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetAllByModel
{
    public record GetAllAdsByModelCommand(string Model, int pageNumber) : IRequest<Result<Paginated<AdvertisementResponse>>>, ICacheable
    {
        public string CacheKey => $"Ads:{Model}:{pageNumber}";

        public int DurationInSeconds => 30;
    }

    internal class GetAllAdsByModelCommandHandler : IRequestHandler<GetAllAdsByModelCommand, Result<Paginated<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllAdsByModelCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Result<Paginated<AdvertisementResponse>>> Handle(GetAllAdsByModelCommand request, CancellationToken cancellationToken)
        {
            var allAdsByModel = await _advertisementRepository.GetAllByCarModel(request.Model, request.pageNumber);

            var pagedResult = Paginated<AdvertisementResponse>.Create(allAdsByModel.Items, request.pageNumber, 10, allAdsByModel.TotalRecords);

            return Result.Success(pagedResult);
        }
    }

}