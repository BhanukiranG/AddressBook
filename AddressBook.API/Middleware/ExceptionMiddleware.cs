namespace AddressBook.API.Middleware;

using Application.DTOs;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Call next middleware / controller
        }
        catch (Exception ex)
        {
            // Optionally log exception
            Console.WriteLine(ex); // replace with ILogger in production

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiErrorResponse
            {
                Message = "Internal Server Error",
                Successful = false
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}