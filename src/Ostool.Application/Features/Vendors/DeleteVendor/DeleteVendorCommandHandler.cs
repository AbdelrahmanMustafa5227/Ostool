using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.DeleteVendor
{
    public record DeleteVendorCommand(Guid VendorId) : IRequest<Result>;

    internal class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result>
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public DeleteVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _vendorRepository = vendorRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetById(request.VendorId);

            if (vendor == null)
            {
                return Result.Failure(new Error("Could Not Find a vendor with Id", HttpStatusCode.NotFound, "Resourse Not Found"));
            }

            _vendorRepository.Delete(vendor);
            await _unitOfWork.SaveChangesAsync();
            await _publisher.Publish(new VendorsCacheInvalidationEvent());
            return Result.Success();
        }
    }
}