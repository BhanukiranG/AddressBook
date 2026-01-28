using AutoMapper;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class ContactService(IContactRepository repository, IMapper mapper) : IContactService
    {
        public async Task<int> CreateAsync(CreateContactDto dto)
        {
            var entity = mapper.Map<Contacts>(dto);
            return await repository.AddAsync(entity);
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ContactResponseDto>>(entities);
        }

        public async Task<ContactResponseDto?> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            return entity == null
                ? null
                : mapper.Map<ContactResponseDto>(entity);
        }

        public async Task<bool> UpdateAsync(UpdateContactDto dto)
        {
            var entity = await repository.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Email != null) entity.Email = dto.Email;
            if (dto.Phone != null) entity.Phone = dto.Phone;

            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}