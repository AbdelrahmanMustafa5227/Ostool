using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Visitors.UnsaveAdvertisement
{
    public record UnsaveAdvertisementCommand(Guid Id) : IRequest<Result>;

    internal class UnsaveAdvertisementCommandHandler : IRequestHandler<UnsaveAdvertisementCommand, Result>
    {
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnsaveAdvertisementCommandHandler(IFavouritesRepository favouritesRepository, IUnitOfWork unitOfWork)
        {
            _favouritesRepository = favouritesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UnsaveAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var ad = await _favouritesRepository.GetById(request.Id);

            if(ad is null)
                return Result.Failure(Error.NotFound);

            _favouritesRepository.Delete(ad);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
