using AddressBook.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AddressBook.API.Filters
{
    public class ApiResponseWrapperFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
                switch (objectResult.Value)
                {
                    // Ignore already wrapped responses
                    case ApiResponse<object>:
                        await next();
                        return;

                    // Handle null → 404
                    case null:
                        context.Result = new ObjectResult(new ApiResponse<object>
                        {
                            Successful = false,
                            Message = "Resource not found"
                        })
                        {
                            StatusCode = StatusCodes.Status404NotFound
                        };

                        await next();
                        return;

                    default:
                        // SUCCESS
                        context.Result = new ObjectResult(new ApiResponse<object>
                        {
                            Data = objectResult.Value,
                            Successful = true
                        })
                        {
                            StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK
                        };

                        break;
                }

            await next();
        }
    }
}