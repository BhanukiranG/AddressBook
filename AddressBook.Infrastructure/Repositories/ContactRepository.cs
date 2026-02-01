using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using PetaPoco;

namespace AddressBook.Infrastructure.Repositories
{
    public class ContactRepository(IDatabase database) : IContactRepository
    {
        public async Task<int> AddContactAsync(Contacts contacts)
        {
            contacts.CreatedAt = DateTime.UtcNow;
            var id = await database.InsertAsync(contacts);
            return Convert.ToInt32(id);
        }

        public async Task<IEnumerable<Contacts>> GetContactsAsync()
        {
            return await database.FetchAsync<Contacts>("SELECT * FROM Contacts");
        }

        public async Task<Contacts?> GetContactAsync(int id)
        {
            return await database.SingleOrDefaultAsync<Contacts>(id);
        }

        public async Task<bool> UpdateContactAsync(Contacts contacts)
        {
            var existingContact = await database.SingleOrDefaultAsync<Contacts>(contacts.Id);
            if (existingContact == null) return false;

            if (!string.IsNullOrWhiteSpace(contacts.Name)) existingContact.Name = contacts.Name;
            if (!string.IsNullOrWhiteSpace(contacts.Email)) existingContact.Email = contacts.Email;
            if (!string.IsNullOrWhiteSpace(contacts.Phone)) existingContact.Phone = contacts.Phone;
            existingContact.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await database.UpdateAsync(existingContact);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var affectedRows = await database.DeleteAsync<Contacts>(id);
            return affectedRows > 0;
        }
    }
}