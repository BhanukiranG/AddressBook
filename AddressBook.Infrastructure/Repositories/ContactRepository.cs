using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using PetaPoco;

namespace AddressBook.Infrastructure.Repositories
{
    public class ContactRepository(IDatabase database) : IContactRepository
    {
        public Task<int> AddAsync(Contacts contacts)
        {
            contacts.CreatedAt = DateTime.UtcNow;
            var id = (int)database.Insert(contacts);
            return Task.FromResult(id);
        }

        public Task<IEnumerable<Contacts>> GetAllAsync()
        {
            var result = database.Fetch<Contacts>("SELECT * FROM Contacts");
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<Contacts?> GetByIdAsync(int id)
        {
            var contact = (Contacts?)database.SingleOrDefault<Contacts>(id);
            return Task.FromResult(contact);
        }

        public Task<bool> UpdateAsync(Contacts contacts)
        {
            var existingContact = database.SingleOrDefault<Contacts>(contacts.Id);
            if (existingContact == null) return Task.FromResult(false);

            if (!string.IsNullOrWhiteSpace(contacts.Name)) existingContact.Name = contacts.Name;
            if (!string.IsNullOrWhiteSpace(contacts.Email)) existingContact.Email = contacts.Email;
            if (!string.IsNullOrWhiteSpace(contacts.Phone)) existingContact.Phone = contacts.Phone;
            existingContact.UpdatedAt = DateTime.UtcNow;

            var affectedRows = database.Update(existingContact);
            return Task.FromResult(affectedRows > 0);
        }

        public Task<bool> DeleteAsync(int id)
        {
            int affectedRows = database.Delete<Contacts>(id);
            return Task.FromResult(affectedRows > 0);
        }
    }
}