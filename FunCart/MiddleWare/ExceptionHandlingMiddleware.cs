using FunCart.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FunCart.MiddleWare
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = "Unhandled exception.";

            switch (exception)
            {
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    error = exception.Message;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    error = string.IsNullOrEmpty(exception.Message) ? "Not Authorized." : exception.Message;
                    break;
              
                default:
                    break;
            }

            var result = JsonConvert.SerializeObject(new { code = code, message = error });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
