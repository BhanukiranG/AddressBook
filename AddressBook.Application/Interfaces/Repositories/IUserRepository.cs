using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Users?> GetUserByEmailAsync(string email);
        Task<int> AddUserAsync(Users user);
    }
}