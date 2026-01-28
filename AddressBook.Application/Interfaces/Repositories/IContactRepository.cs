using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<int> AddAsync(Contacts contacts);
        Task<IEnumerable<Contacts>> GetAllAsync();
        Task<Contacts?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Contacts contacts);
        Task<bool> DeleteAsync(int id);
    }
}