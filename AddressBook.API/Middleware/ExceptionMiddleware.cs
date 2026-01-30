using AddressBook.Application.Common.Exceptions;
using AddressBook.Application.DTOs;
using System.Net;
using System.Text.Json;

namespace AddressBook.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteError(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await WriteError(
                    context,
                    "Internal Server Error",
                    HttpStatusCode.InternalServerError);
            }
        }

        static private async Task WriteError(
            HttpContext context,
            string message,
            HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ApiResponse<object>
            {
                Successful = false,
                Message = message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}