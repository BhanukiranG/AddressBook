namespace AddressBook.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string email, string role);
    }
}