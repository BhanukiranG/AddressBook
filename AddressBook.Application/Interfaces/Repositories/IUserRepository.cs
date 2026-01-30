using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<int> AddAsync(Users user);
    }
}