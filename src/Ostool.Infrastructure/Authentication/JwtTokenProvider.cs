using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ostool.Application.Abstractions.Authentication;
using Ostool.Application.Helpers;
using Ostool.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtOptions _options;
        private readonly AppDbContext _dbContext;

        public JwtTokenProvider(IOptions<JwtOptions> options, AppDbContext dbContext)
        {
            _options = options.Value;
            _dbContext = dbContext;
        }


        public JwtToken GenerateJwtToken(Guid userId)
        {
            var accessToken = GenerateAccessToken(userId);
            var refreshToken = PersistRefreshToken(userId);
            return new JwtToken(accessToken, refreshToken);
        }

        public async Task<Result<JwtToken>> RefreshAccessToken(Guid refreshToken)
        {
            // Get refresh token from DB
            var TokenFromDb = await _dbContext.Set<RefreshToken>().FirstOrDefaultAsync(u => u.Token == refreshToken);
            if (TokenFromDb == null)
                return Result.Failure<JwtToken>(new Error("Invalid Refresh Token"));

            // Check if the refresh token is expired 
            if (TokenFromDb.ExpiresIn < DateTime.UtcNow)
                return Result.Failure<JwtToken>(new Error("Expired Refresh Token"));

            // Generate new Access Tokens
            var accessToken = GenerateAccessToken(TokenFromDb.UserId);

            return Result.Success(new JwtToken(accessToken, TokenFromDb.Token));
        }

        public async Task Revoke(Guid UserId)
        {
            await _dbContext.Set<RefreshToken>().Where(x => x.UserId == UserId).ExecuteDeleteAsync();
        }


        private string GenerateAccessToken(Guid userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub , userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                Audience = _options.Audience,
                Issuer = _options.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_options.AccessExpiresInMinutes)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Guid PersistRefreshToken(Guid UserId)
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid(),
                UserId = UserId,
                ExpiresIn = DateTime.UtcNow.AddMinutes(_options.RefreshExpiresInMinutes)
            };

            _dbContext.Set<RefreshToken>().Add(refreshToken);
            _dbContext.SaveChanges();
            return refreshToken.Token;
        }
    }
}