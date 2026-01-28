using AddressBook.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> Success<T>(T data, int statusCode = 200)
            => StatusCode(statusCode, new ApiResponse<T>
            {
                Data = data,
                Successful = true
            });

        protected ActionResult<ApiResponse<T>> Failure<T>(string message, int statusCode = 400)
            => StatusCode(statusCode, new ApiResponse<T>
            {
                Data = default,
                Successful = false,
                Message = message
            });
    }
}