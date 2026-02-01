using AddressBook.Application.DTOs.Contact;

namespace AddressBook.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<int> CreateContactAsync(CreateContactDto dto);
        Task<IEnumerable<ContactResponseDto>> GetContactsAsync();
        Task<ContactResponseDto?> GetContactAsync(int id);
        Task<bool> UpdateContactAsync(UpdateContactDto dto);
        Task<bool> DeleteContactAsync(int id);
    }
}