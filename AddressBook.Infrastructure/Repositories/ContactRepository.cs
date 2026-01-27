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

        public Task<int> AddAsync(Contact contact)
        {
            contact.CreatedAt = DateTime.UtcNow;
            var id = (int)_database.Insert(contact);
            return Task.FromResult(id);
        }

        public Task<IEnumerable<Contact>> GetAllAsync()
        {
            var result = _database.Fetch<Contact>("SELECT * FROM Contacts");
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<Contact?> GetByIdAsync(int id)
        {
            var contact = _database.SingleOrDefault<Contact>(
                "SELECT * FROM Contacts WHERE Id=@0", id);
            return Task.FromResult(contact);
        }

        public Task<bool> UpdateAsync(Contact contact)
        {
            contact.UpdatedAt = DateTime.UtcNow;
            var rows = _database.Update(contact);
            return Task.FromResult(rows > 0);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var rows = _database.Execute(
                "DELETE FROM Contacts WHERE Id=@0", id);
            return Task.FromResult(rows > 0);
        }
    }
}