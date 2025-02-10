using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.PostAd
{
    public record PostAdCommand(Guid VendorId, Guid CarId, string? Description,
        decimal Price, int Year, DateTime? ExpirationDate) : IRequest<Result>;


    internal class PostAdCommandHandler : IRequestHandler<PostAdCommand, Result>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ICarRepository _carRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostAdCommandHandler(IAdvertisementRepository advertisementRepository, IUnitOfWork unitOfWork, ICarRepository carRepository, IVendorRepository vendorRepository)
        {
            _advertisementRepository = advertisementRepository;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<Result> Handle(PostAdCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.CarId);
            if (car == null)
                return Result.Failure(new Error("Car not found", HttpStatusCode.NotFound, "NotFound"));

            var vendor = await _vendorRepository.GetById(request.VendorId);
            if (vendor == null)
                return Result.Failure(new Error("Vendor not found", HttpStatusCode.NotFound, "NotFound"));

            _advertisementRepository.Add(request.ToModel());
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}