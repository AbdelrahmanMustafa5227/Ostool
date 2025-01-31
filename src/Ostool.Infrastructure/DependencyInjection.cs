using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Infrastructure.Idempotency;
using Ostool.Infrastructure.Persistence;
using Ostool.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarSpecsRepository, CarSpecsRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<IdempotencyService>();
            return services;
        }
    }
}