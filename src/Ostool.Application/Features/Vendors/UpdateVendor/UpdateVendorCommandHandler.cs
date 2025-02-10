using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.UpdateVendor
{
    public record UpdateVendorCommand(Guid Id, string Name, string ContactNumber, string Email) : IRequest<Result>;

    internal class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Result>
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
        {
            _vendorRepository = vendorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetById(request.Id);
            if (vendor == null)
                return Result.Failure(new Error("A Vendor With Supplied Id Couldn't be found", HttpStatusCode.NotFound, "Resourse Not Found"));

            vendor.ApplyChanges(request);
            _vendorRepository.Update(vendor);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}