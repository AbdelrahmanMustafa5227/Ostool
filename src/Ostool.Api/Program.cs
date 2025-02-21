using Microsoft.Extensions.Hosting;
using Ostool.Api.Helper;
using Ostool.Api.Middlewares;
using Ostool.Application;
using Ostool.Application.Abstractions;
using Ostool.Infrastructure;
using Serilog;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(cfg => cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddProblemDetails(cfg =>
{
    cfg.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});

builder.Services.AddCors(cfg =>
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

builder.Host.UseSerilog((context, cfg) =>
{
    cfg.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseCors("MyPolicy");
app.Run();