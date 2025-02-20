using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ostool.Application.Abstractions.Authentication;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Infrastructure.Authentication;
using Ostool.Infrastructure.Idempotency;
using Ostool.Infrastructure.Persistence;
using Ostool.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeX.Infrastructure.Authentication;

namespace Ostool.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarSpecsRepository, CarSpecsRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IdempotencyService>();

            //services.AddHostedService<TokenRefresherJob>();
            //services.AddTransient<RefreshTokenContext>();

            services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));
            services.ConfigureOptions<AuthOptionsSetup>();
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddJwtBearer("Bearer")
                .AddOAuth("Google", o =>
                {
                    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    o.ClientId = "536185920438-ep2nq7cgald1avipu16tudr9n8dqlg7d.apps.googleusercontent.com";
                    o.ClientSecret = "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";
                    o.CallbackPath = "/signin-google";

                    o.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
                    o.AuthorizationEndpoint += "?prompt=consent&access_type=offline";
                    o.TokenEndpoint = "https://accounts.google.com/o/oauth2/token";
                    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";

                    o.Scope.Add("openid");
                    o.Scope.Add("profile");
                    o.Scope.Add("email");
                    o.SaveTokens = true;

                    o.Events.OnCreatingTicket = OAuthEvents.RegisterUser;
                    o.Events.OnRemoteFailure = ctx =>
                    {
                        ctx.HandleResponse();
                        ctx.Response.Redirect("/auth/GoogleAuthError?message=" + ctx.Failure!.Message);
                        return Task.CompletedTask;
                    };

                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey("verified_email", "verified_email");
                    o.ClaimActions.MapJsonKey("picture", "picture");

                });

            return services;
        }
    }
}