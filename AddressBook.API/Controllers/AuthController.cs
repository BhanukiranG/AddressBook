using AddressBook.Application.DTOs;
using AddressBook.Application.DTOs.Auth;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register(RegisterDto dto)
        {
            var result = await authService.RegisterAsync(dto);

            if (!result.Success)
                return Failure<AuthResponseDto>(result.Message!);

            return Success(new AuthResponseDto
            {
                Token = result.Token!
            }, 201);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginDto dto)
        {
            var result = await authService.LoginAsync(dto);

            if (!result.Success)
                return Failure<AuthResponseDto>(result.Message!, 401);

            return Success(new AuthResponseDto
            {
                Token = result.Token!
            });
        }
    }
}