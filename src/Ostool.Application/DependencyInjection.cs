using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingBehaviour<,>));

            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
            services.AddTransient(typeof(ITestableLogger<>), typeof(TestableLogger<>));
            services.AddMemoryCache();
            return services;
        }
    }
}