using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Auth.LocalRegister
{
    public record LocalRegisterCommand(string UserName, string Email, string Password, int role) : IRequest<Result>;

    internal class LocalRegisterCommandHandler : IRequestHandler<LocalRegisterCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocalRegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(LocalRegisterCommand request, CancellationToken cancellationToken)
        {
            var appUser = request.ToAppUser();

            if (await _userRepository.IsEmailUsed(request.Email))
                return Result.Failure(new Error("This Email Is Used", HttpStatusCode.Conflict, "Conflict"));

            if (appUser is Visitor visitor)
                _userRepository.Register(visitor);
            else if (appUser is Vendor vendor)
                _userRepository.Register(vendor);

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}