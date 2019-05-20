using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.API.Models;
using WebApi.BusinessLogic.Exceptions;

namespace WebApi.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string errorMessage = "Internal Server Error.";

            if (exception is BaseException baseException)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = baseException.Message;
            }

            if (exception is NotFoundException notFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
