using AddressBook.Application.DTOs.Auth;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Security;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class AuthService(IUserRepository userRepository, IJwtTokenGenerator jwt) : IAuthService
    {
        public async Task<AuthResult> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return new AuthResult
                {
                    Success = false,
                    Message = "Email already exists"
                };

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new Users
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User"
            };

            await userRepository.AddAsync(user);

            var token = jwt.GenerateToken(user.Id, user.Email, user.Role);

            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        public async Task<AuthResult> LoginAsync(LoginDto dto)
        {
            var user = await userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid credentials"
                };

            var token = jwt.GenerateToken(user.Id, user.Email, user.Role);

            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }
    }
}