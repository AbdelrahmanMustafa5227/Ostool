using Ostool.Application.Abstractions.Cache;
using Ostool.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.GetAll
{
    public record GetAllVendorsCommand(int pageNumber) : IRequest<Result<Paginated<GetAllVendorsResponse>>>, ICacheable
    {
        public string CacheKey => $"All-Vendors:{pageNumber}";
        public int DurationInSeconds => 10 * 60;
    }

    public record GetAllVendorsResponse(Guid Id, string Name, string ContactNumber, string Email);

    internal class GetAllVendorsCommandHandler : IRequestHandler<GetAllVendorsCommand, Result<Paginated<GetAllVendorsResponse>>>
    {
        private readonly IVendorRepository _vendorRepository;

        public GetAllVendorsCommandHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<Result<Paginated<GetAllVendorsResponse>>> Handle(GetAllVendorsCommand request, CancellationToken cancellationToken)
        {
            var vendors = await _vendorRepository.GetAll(request.pageNumber);

            var vendorsResponse = vendors.Items.Select(x => new GetAllVendorsResponse(x.Id, x.VendorName, x.ContactNumber, x.Email)).ToList();

            var pagedResult = Paginated<GetAllVendorsResponse>.Create(vendorsResponse, request.pageNumber, 10, vendors.TotalRecords);

            return Result.Success(pagedResult);
        }
    }
}