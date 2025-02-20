using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Vendors;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.AddVendor
{
    public record AddVendorCommand(string Name, string ContactNumber, string Email) : IRequest<Result<AddVendorCommandResponse>>;

    public record AddVendorCommandResponse(Guid Id, string Name, string ContactNumber, string Email);

    internal class AddVendorCommandHandler : IRequestHandler<AddVendorCommand, Result<AddVendorCommandResponse>>
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public AddVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _vendorRepository = vendorRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result<AddVendorCommandResponse>> Handle(AddVendorCommand request, CancellationToken cancellationToken)
        {
            if (await _vendorRepository.Exists(request.Email))
                return Result.Failure<AddVendorCommandResponse>(new Error("Email Already Used", HttpStatusCode.Conflict, "Conflict Error"));

            var vendorModel = request.ToModel();
            _vendorRepository.Add(vendorModel);
            await _unitOfWork.SaveChangesAsync();
            await _publisher.Publish(new VendorsCacheInvalidationEvent());

            var response = new AddVendorCommandResponse(vendorModel.Id, vendorModel.VendorName, vendorModel.ContactNumber, vendorModel.Email);
            return response;
        }
    }
}