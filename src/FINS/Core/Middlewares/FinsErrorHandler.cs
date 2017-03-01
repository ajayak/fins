using System;
using System.Net;
using System.Threading.Tasks;
using FINS.Core.FinsExceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FINS.Core
{
    public class FinsErrorHandler
    {
        private readonly RequestDelegate _next;

        public FinsErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // must be awaited
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            if (exception is FinsNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is FinsInvalidOperation) code = HttpStatusCode.BadRequest;
            else if (exception is FinsInvalidDataException) code = HttpStatusCode.BadRequest;

            return WriteExceptionAsync(context, exception, code);
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            return response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            }));
        }
    }
}
