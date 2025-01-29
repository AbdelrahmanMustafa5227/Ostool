using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Exceptions;
using System.Net;

namespace Ostool.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                var problemDetails = ex switch
                {
                    ValidationFailureException ve => new ProblemDetails()
                    {
                        Title = "Validation Faliure",
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = ve.Message,
                        Extensions = new Dictionary<string, object?>() { { "Failures", ve.ValidationFailures } }
                    },
                    _ => new ProblemDetails()
                    {
                        Title = "Server Error",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = ex.Message,
                    }
                };

                problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
                context.Response.StatusCode = (int)problemDetails.Status!;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}