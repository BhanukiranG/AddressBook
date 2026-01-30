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
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Successful = false,
                    Message = context.RequestServices
                        .GetRequiredService<IWebHostEnvironment>()
                        .IsDevelopment()
                        ? ex.Message
                        : "Internal Server Error"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}