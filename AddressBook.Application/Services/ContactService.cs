using AddressBook.Application.Common.Exceptions;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Domain.Entities;
using AutoMapper;

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

            return entity is null
                ? throw new NotFoundException("Contact not found")
                : mapper.Map<ContactResponseDto>(entity);
        }

        public async Task<bool> UpdateAsync(UpdateContactDto dto)
        {
            var entity = await repository.GetByIdAsync(dto.Id);

            if (entity is null)
                throw new NotFoundException("Contact not found");

            mapper.Map(dto, entity);

            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await repository.DeleteAsync(id);

            return !deleted
                ? throw new NotFoundException("Contact not found")
                : true;
        }
    }
}