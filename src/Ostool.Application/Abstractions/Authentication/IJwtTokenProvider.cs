using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Authentication
{
    public interface IJwtTokenProvider
    {
        JwtToken GenerateJwtToken(Guid userId);
        Task<Result<JwtToken>> RefreshAccessToken(Guid refreshToken);
        Task Revoke(Guid UserId);
    }
}