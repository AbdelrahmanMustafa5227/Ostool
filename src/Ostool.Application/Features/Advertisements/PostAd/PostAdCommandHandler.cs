using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.PostAd
{
    public record PostAdCommand(Guid VendorId, Guid CarId, string? Description,
        decimal Price, int Year, DateTime? ExpirationDate) : IRequest<Result<PostAdCommandResponse>>;

    public record PostAdCommandResponse(Guid Id, Guid VendorId, Guid CarId, string? Description,
        decimal Price, int Year, DateTime? ExpirationDate);

    internal class PostAdCommandHandler : IRequestHandler<PostAdCommand, Result<PostAdCommandResponse>>
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

        public async Task<Result<PostAdCommandResponse>> Handle(PostAdCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetById(request.CarId);
            if (car == null)
                return Result.Failure<PostAdCommandResponse>(new Error("Car not found", HttpStatusCode.NotFound, "NotFound"));

            var vendor = await _vendorRepository.GetById(request.VendorId);
            if (vendor == null)
                return Result.Failure<PostAdCommandResponse>(new Error("Vendor not found", HttpStatusCode.NotFound, "NotFound"));

            var ad = request.ToModel();
            _advertisementRepository.Add(ad);
            await _unitOfWork.SaveChangesAsync();

            var response = new PostAdCommandResponse(ad.Id, ad.VendorId, ad.CarId, ad.Description, ad.Price, ad.Year, ad.ExpirationDate);
            return response;
        }
    }
}