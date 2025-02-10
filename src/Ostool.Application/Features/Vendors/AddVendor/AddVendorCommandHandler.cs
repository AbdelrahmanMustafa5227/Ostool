using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.AddVendor
{
    public record AddVendorCommand(string Name, string ContactNumber, string Email) : IRequest<Result>;

    internal class AddVendorCommandHandler : IRequestHandler<AddVendorCommand, Result>
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
        {
            _vendorRepository = vendorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(AddVendorCommand request, CancellationToken cancellationToken)
        {
            if (await _vendorRepository.Exists(request.Email))
                return Result.Failure(new Error("Email Already Used", HttpStatusCode.Conflict, "Conflict Error"));

            _vendorRepository.Add(request.ToModel());
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}