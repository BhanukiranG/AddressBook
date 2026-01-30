using AddressBook.Application.DTOs.Auth;

namespace AddressBook.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterDto dto);

        Task<AuthResult> LoginAsync(LoginDto dto);
    }
}