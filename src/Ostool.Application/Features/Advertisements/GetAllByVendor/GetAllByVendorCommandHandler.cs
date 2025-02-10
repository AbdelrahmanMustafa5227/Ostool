using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.GetAllByVendor
{
    public record GetAllByVendorCommand(string VendorName) : IRequest<Result<List<AdvertisementResponse>>>;

    internal class GetAllByVendorCommandHandler : IRequestHandler<GetAllByVendorCommand, Result<List<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllByVendorCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Result<List<AdvertisementResponse>>> Handle(GetAllByVendorCommand request, CancellationToken cancellationToken)
        {
            var ads = await _advertisementRepository.GetAllByVendor(request.VendorName);
            return Result.Success(ads);
        }
    }
}