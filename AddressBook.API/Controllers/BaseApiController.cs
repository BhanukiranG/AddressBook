namespace AddressBook.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected IActionResult Success<T>(T data, int statusCode = 200)
    {
        return StatusCode(statusCode, new ApiResponse<T>
        {
            Data = data,
            Successful = true
        });
    }

    protected IActionResult Failure(string message, int statusCode = 400)
    {
        return StatusCode(statusCode, new ApiErrorResponse
        {
            Message = message,
            Successful = false
        });
    }
}