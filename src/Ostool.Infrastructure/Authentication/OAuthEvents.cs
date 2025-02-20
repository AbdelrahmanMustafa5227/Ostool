using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    public static class OAuthEvents
    {
        public static async Task RegisterUser(OAuthCreatingTicketContext ctx)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
                var result = await ctx.Backchannel.SendAsync(request);

                var claims = await result.Content.ReadFromJsonAsync<JsonElement>();
                var AuthenticatedUser = JsonSerializer.Deserialize<GoogeUserInfo>(claims.GetRawText());
                ctx.RunClaimActions(claims);

                var db = ctx.HttpContext
                    .RequestServices
                    .GetRequiredService<IUserRepository>();

                var protector = ctx.HttpContext
                    .RequestServices
                    .GetRequiredService<IDataProtectionProvider>()
                    .CreateProtector("claims");

                var UserExists = await db.IsEmailUsed(AuthenticatedUser!.Email);
                var EncryptedUser = protector.Protect(JsonSerializer.Serialize(AuthenticatedUser));

                ctx.Properties.IsPersistent = true;
                ctx.Properties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10);
                var role = ctx.Properties.Items.TryGetValue("role", out var roleValue) ? int.Parse(roleValue!) : 0;

                if (!UserExists)
                    ctx.Properties.RedirectUri = $"/auth/GoogleCallback?code={EncryptedUser}&role={role}";

            }
            catch (Exception e)
            {
                ctx.Fail(e);
            }

        }

    }
}