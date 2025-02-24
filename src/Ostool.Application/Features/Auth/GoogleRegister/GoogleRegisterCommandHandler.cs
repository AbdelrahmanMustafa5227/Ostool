using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Auth.GoogleRegister
{
    public record GoogleRegisterCommand(Guid Id, string Email, string Name, int role) : IRequest<Result>;

    internal class GoogleRegisterCommandHandler : IRequestHandler<GoogleRegisterCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleRegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GoogleRegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.role == 0)
            {
                var visitor = new Visitor
                {
                    Email = request.Email,
                    UserName = request.Name,
                    Id = request.Id,
                    SubscribedToEmails = false,
                    AuthProvider = AuthProvider.Google
                };

                _userRepository.Register(visitor);
            }
            else
            {
                var vendor = new Vendor
                {
                    Email = request!.Email,
                    UserName = request.Name,
                    Id = request.Id,
                    AuthProvider = AuthProvider.Google
                };
                _userRepository.Register(vendor);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}