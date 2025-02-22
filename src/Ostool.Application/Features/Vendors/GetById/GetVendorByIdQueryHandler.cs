using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.GetById
{
    public record GetVendorByIdQuery(Guid Id) : IRequest<Result<GetVendorByIdResponse>>;

    public record GetVendorByIdResponse(Guid Id, string Name, string ContactNumber, string Email);

    internal class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, Result<GetVendorByIdResponse>>
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ITestableLogger<GetVendorByIdQueryHandler> _logger;

        public GetVendorByIdQueryHandler(IVendorRepository vendorRepository, ITestableLogger<GetVendorByIdQueryHandler> logger)
        {
            _vendorRepository = vendorRepository;
            _logger = logger;
        }

        public async Task<Result<GetVendorByIdResponse>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetById(request.Id);
            if (vendor == null)
            {
                _logger.LogError("Vendor with an Id of {0} is not found", default, request.Id);
                return Result.Failure<GetVendorByIdResponse>(new Error("Vendor not found", HttpStatusCode.NotFound, "Resourse not found"));
            }

            _logger.LogInformation("Fetched a Vendor from the database with an Id = {0}", vendor.Id);
            return Result.Success(new GetVendorByIdResponse(vendor.Id, vendor.VendorName, vendor.ContactNumber, vendor.Email));
        }
    }
}