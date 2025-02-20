using Ostool.Application.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Auth.LocalRefresh
{
    public record LocalRefreshCommand(Guid RefreshToken) : IRequest<Result<JwtToken>>;

    internal class LocalRefreshCommandHandler : IRequestHandler<LocalRefreshCommand, Result<JwtToken>>
    {
        private readonly IJwtTokenProvider _jwtProvider;

        public LocalRefreshCommandHandler(IJwtTokenProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<JwtToken>> Handle(LocalRefreshCommand request, CancellationToken cancellationToken)
        {
            return await _jwtProvider.RefreshAccessToken(request.RefreshToken);

        }
    }
}