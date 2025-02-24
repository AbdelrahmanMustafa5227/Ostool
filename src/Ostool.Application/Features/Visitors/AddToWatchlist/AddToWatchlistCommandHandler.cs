using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Visitors.AddToWatchlist
{
    public record AddToWatchlistCommand(Guid VisitorId, Guid CarId) : IRequest<Result>;

    internal class AddToWatchlistCommandHandler : IRequestHandler<AddToWatchlistCommand, Result>
    {
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddToWatchlistCommandHandler(IWatchlistRepository watchlistRepository, IUnitOfWork unitOfWork, ICarRepository carRepository, IUserRepository userRepository)
        {
            _watchlistRepository = watchlistRepository;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AddToWatchlistCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.VisitorExists(request.VisitorId))
                return Result.Failure(Error.NotFound);

            if (await _carRepository.GetById(request.CarId) is null)
                return Result.Failure(Error.NotFound);

            _watchlistRepository.Add(new WatchList
            {
                CarId = request.CarId,
                VisitorId = request.VisitorId
            });

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}