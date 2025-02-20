using Ostool.Application.Abstractions;
using Ostool.Application.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Auth.LocalRevoke
{
    public record LocalRevokeCommand(Guid userId) : IRequest<Result>;

    internal class LocalRevokeCommandHandler : IRequestHandler<LocalRevokeCommand, Result>
    {
        private readonly IUserContext _userContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public LocalRevokeCommandHandler(IUserContext userContext, IJwtTokenProvider jwtTokenProvider)
        {
            _userContext = userContext;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<Result> Handle(LocalRevokeCommand request, CancellationToken cancellationToken)
        {
            if (_userContext.UserId == null || _userContext.UserId != request.userId)
                return Result.Failure(new Error("You are not allowed to do this", HttpStatusCode.Forbidden));

            await _jwtTokenProvider.Revoke(request.userId);
            return Result.Success();
        }
    }
}