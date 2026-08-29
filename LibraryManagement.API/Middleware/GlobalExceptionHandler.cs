using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) //bu parametrelieri incele
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            if(exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }

            else if (exception is KeyNotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
            }

            else if(exception is UnauthorizedAccessException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
            }

            else if (exception is InvalidOperationException)
            {
                statusCode = StatusCodes.Status409Conflict;
            }

            httpContext.Response.StatusCode = statusCode;

            var response = new
            {
                error = exception.Message,
                statusCode = statusCode
            };

            await httpContext.Response.WriteAsJsonAsync(response);

            return true; // niye true dondu ?
        }

         
        }
    }



//class builder ile class farki solution new project bunlara bak