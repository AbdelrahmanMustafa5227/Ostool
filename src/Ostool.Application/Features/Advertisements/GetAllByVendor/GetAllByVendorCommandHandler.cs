﻿using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetAllByVendor
{
    public record GetAllByVendorCommand(string VendorName, int pageNumber) : IRequest<Result<Paginated<AdvertisementResponse>>>, ICacheable
    {
        public string CacheKey => $"Ads:{VendorName}:{pageNumber}";

        public int DurationInSeconds => 30;
    }

    internal class GetAllByVendorCommandHandler : IRequestHandler<GetAllByVendorCommand, Result<Paginated<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllByVendorCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Result<Paginated<AdvertisementResponse>>> Handle(GetAllByVendorCommand request, CancellationToken cancellationToken)
        {
            var ads = await _advertisementRepository.GetAllByVendor(request.VendorName, request.pageNumber);

            var pagedResult = Paginated<AdvertisementResponse>.Create(ads.Items, request.pageNumber, 10, ads.TotalRecords);

            return Result.Success(pagedResult);
        }
    }
}