using Ostool.Api.Filters;
using Ostool.Api.Helper;
using Ostool.Application.Abstractions;

namespace Ostool.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddHttpClient();
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

            // Service Filters
            services.AddScoped<Idempotent>();

            services.AddProblemDetails(cfg =>
            {
                cfg.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                };
            });

            services.AddCors(cfg =>
            {
                cfg.AddPolicy("MyPolicy", policyBuilder =>
                {
                    policyBuilder
                    .AllowAnyOrigin()
                    .WithMethods("GET", "POST")
                    .WithHeaders("Content-Type", "Authorization");
                    //.AllowCredentials();
                });
            });

            return services;
        }
    }
}