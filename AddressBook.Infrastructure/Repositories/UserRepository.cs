using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using PetaPoco;

namespace AddressBook.Infrastructure.Repositories
{
    public class UserRepository(IDatabase db) : IUserRepository
    {
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await db.SingleOrDefaultAsync<Users>("WHERE Email = @0", email);
        }

        public async Task<int> AddUserAsync(Users user)
        {
            user.CreatedAt = DateTime.UtcNow;
            return (int)await db.InsertAsync(user);
        }
    }
}