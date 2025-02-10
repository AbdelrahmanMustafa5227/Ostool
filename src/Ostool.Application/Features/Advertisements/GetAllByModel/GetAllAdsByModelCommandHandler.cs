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
    public record GetAllAdsByModelCommand(string Model) : IRequest<Result<List<AdvertisementResponse>>>;

    internal class GetAllAdsByModelCommandHandler : IRequestHandler<GetAllAdsByModelCommand, Result<List<AdvertisementResponse>>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public GetAllAdsByModelCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Result<List<AdvertisementResponse>>> Handle(GetAllAdsByModelCommand request, CancellationToken cancellationToken)
        {
            var allAdsByModel = await _advertisementRepository.GetAllByCarModel(request.Model);
            return Result.Success(allAdsByModel);
        }
    }

}