using Microsoft.Extensions.Hosting;
using Ostool.Api.Middlewares;
using Ostool.Application;
using Ostool.Infrastructure;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(cfg => cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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
        .WithOrigins("asd")
        .WithMethods("GET", "POST")
        .WithHeaders("Content-Type", "Authorization")
        .AllowCredentials();
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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseCors("MyPolicy");
app.Run();