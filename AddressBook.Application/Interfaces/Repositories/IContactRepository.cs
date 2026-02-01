using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<int> AddContactAsync(Contacts contacts);
        Task<IEnumerable<Contacts>> GetContactsAsync();
        Task<Contacts?> GetContactAsync(int id);
        Task<bool> UpdateContactAsync(Contacts contacts);
        Task<bool> DeleteContactAsync(int id);
    }
}