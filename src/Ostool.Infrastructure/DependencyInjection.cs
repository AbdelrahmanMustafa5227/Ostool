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
            // Database and Persistance
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



            // Authentication
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.Configure<GoogleOAuthOptions>(configuration.GetSection(GoogleOAuthOptions.SectionName));
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<GoogleOAuthOptionsSetup>();
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddJwtBearer("Bearer")
                .AddOAuth("Google", _ => { });


            // Other Services
            services.AddScoped<IdempotencyService>();



            //services.AddHostedService<TokenRefresherJob>();
            //services.AddTransient<RefreshTokenContext>();
            //configuration.GetValue<TimeSpan>("key" , TimeSpan.MaxValue);






            return services;
        }
    }
}