using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using PetaPoco;

namespace AddressBook.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDatabase _database;

        public ContactRepository(IDatabase database)
        {
            _database = database;
        }

        public Task<int> AddAsync(Contacts contacts)
        {
            contacts.CreatedAt = DateTime.UtcNow;
            var id = (int)_database.Insert(contacts);
            return Task.FromResult(id);
        }

        public Task<IEnumerable<Contacts>> GetAllAsync()
        {
            var result = _database.Fetch<Contacts>("SELECT * FROM Contacts");
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<Contacts?> GetByIdAsync(int id)
        {
            var contact = _database.SingleOrDefault<Contacts>(
                "SELECT * FROM Contacts WHERE Id=@0", id);
            return Task.FromResult(contact);
        }

        public async Task<bool> UpdateAsync(Contacts contacts)
        {
            var existing = _database.SingleOrDefault<Contacts>(contacts.Id);
            if (existing == null) return false;

            existing.Name = contacts.Name;
            existing.Email = contacts.Email;
            existing.Phone = contacts.Phone;
            existing.UpdatedAt = DateTime.UtcNow;

            var rows = _database.Update(existing);
            return rows > 0;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var rows = _database.Execute(
                "DELETE FROM Contacts WHERE Id=@0", id);
            return Task.FromResult(rows > 0);
        }
    }
}