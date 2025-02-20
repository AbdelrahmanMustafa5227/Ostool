using Ostool.Application.Abstractions.Authentication;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Auth.LocalLogin
{
    public record LocalLoginCommand(string Email, string Password) : IRequest<Result<JwtToken>>;

    internal class LocalLoginCommandHandler : IRequestHandler<LocalLoginCommand, Result<JwtToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public LocalLoginCommandHandler(IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<Result<JwtToken>> Handle(LocalLoginCommand request, CancellationToken cancellationToken)
        {
            var isVisitor = await _userRepository.IsVisitor(request.Email);
            var isVendor = await _userRepository.IsVendor(request.Email);

            if (!isVendor && !isVisitor)
                return Result.Failure<JwtToken>(new Error("Not Found", HttpStatusCode.NotFound));

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user!.AuthProvider == AuthProvider.Google)
                return Result.Failure<JwtToken>(new Error("Choose appropriate Login Method", HttpStatusCode.Unauthorized));

            if (request.Password != user!.Password)
                return Result.Failure<JwtToken>(new Error("Invalid Credentials", HttpStatusCode.Unauthorized));

            // Revoke any old refresh Tokens 
            await _jwtTokenProvider.Revoke(user.Id);
            return _jwtTokenProvider.GenerateJwtToken(user.Id);
        }
    }
}