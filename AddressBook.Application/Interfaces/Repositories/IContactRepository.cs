using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<int> AddAsync(Contact contact);

        Task<IEnumerable<Contact>> GetAllAsync();

        Task<Contact?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Contact contact);

        Task<bool> DeleteAsync(int id);
    }
}