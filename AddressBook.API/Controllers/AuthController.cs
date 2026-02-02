using AddressBook.Application.DTOs;
using AddressBook.Application.DTOs.Auth;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        /// <summary>
        /// Registers a new user and returns a JWT token.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register(RegisterDto dto)
        {
            var result = await authService.RegisterAsync(dto);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new AuthResponseDto { Token = result.Token! }); 
        }

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginDto dto)
        {
            var result = await authService.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new AuthResponseDto { Token = result.Token! });
        }
    }
}