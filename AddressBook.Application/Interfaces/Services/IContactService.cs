using AddressBook.Application.DTOs.Contact;

namespace AddressBook.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<int> CreateAsync(CreateContactDto dto);

        Task<IEnumerable<ContactResponseDto>> GetAllAsync();

        Task<ContactResponseDto?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(UpdateContactDto dto);

        Task<bool> DeleteAsync(int id);
    }
}