using CheapFlight.Application.Common;
using FluentValidation;
using System.Net;

namespace GlobalFlights.API.middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(
                    ResponseDto<object>.Fail(ex.Errors.Select(e => e.ErrorMessage), "Validation failed"));
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(
                    ResponseDto<object>.Fail(new[] { ex.Message }, "Not found"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    ResponseDto<object>.Fail(new[] { "An unexpected error occurred" }, "Server error"));
            }
        }
    }
}
