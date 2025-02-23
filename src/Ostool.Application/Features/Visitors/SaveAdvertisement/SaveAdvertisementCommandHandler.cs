using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Visitors.SaveAdvertisement
{
    public record SaveAdvertisementCommand(Guid VisitorId , Guid AdId) : IRequest<Result>;

    internal class SaveAdvertisementCommandHandler : IRequestHandler<SaveAdvertisementCommand, Result>
    {
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveAdvertisementCommandHandler(IFavouritesRepository favouritesRepository, IUnitOfWork unitOfWork, IAdvertisementRepository advertisementRepository, IUserRepository userRepository)
        {
            _favouritesRepository = favouritesRepository;
            _unitOfWork = unitOfWork;
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(SaveAdvertisementCommand request, CancellationToken cancellationToken)
        {
            if(await _advertisementRepository.GetById(request.AdId) is null)
            {
                return Result.Failure(Error.NotFound);
            }

            if(!await _userRepository.VisitorExists(request.VisitorId))
            {
                return Result.Failure(Error.NotFound);
            }

            var favourites = new Favourites() 
            { 
                Id = Guid.NewGuid(),
                VisitorId = request.VisitorId,
                AdvertisementId = request.AdId 
            };

            _favouritesRepository.Add(favourites);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
