using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Authentication
{
    internal class GoogleOAuthOptionsSetup : IConfigureNamedOptions<OAuthOptions>
    {
        private readonly GoogleOAuthOptions _options;
        private readonly IConfiguration _configuration;

        public GoogleOAuthOptionsSetup(IOptions<GoogleOAuthOptions> options, IConfiguration configuration)
        {
            _options = options.Value;
            _configuration = configuration;
        }

        public void Configure(OAuthOptions options)
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            options.ClientId = _configuration["Ostool_Google_ClientId"]!;
            options.ClientSecret = _configuration["Ostool_Google_ClientSecret"]!;
            options.CallbackPath = _options.CallbackPath;

            options.AuthorizationEndpoint = _options.AuthorizationEndpoint;
            options.TokenEndpoint = _options.TokenEndpoint;
            options.UserInformationEndpoint = _options.UserInformationEndpoint;

            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.SaveTokens = true;

            options.Events.OnCreatingTicket = OAuthEvents.RegisterUser;
            options.Events.OnRemoteFailure = ctx =>
            {
                ctx.HandleResponse();
                ctx.Response.Redirect("/auth/GoogleAuthError?message=" + ctx.Failure!.Message);
                return Task.CompletedTask;
            };

            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            options.ClaimActions.MapJsonKey("verified_email", "verified_email");
            options.ClaimActions.MapJsonKey("picture", "picture");
        }

        public void Configure(string? name, OAuthOptions options)
        {
            Configure(options);
        }
    }
}