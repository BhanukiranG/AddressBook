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
        public async Task<int> CreateContactAsync(CreateContactDto dto)
        {
            var entity = mapper.Map<Contacts>(dto);
            return await repository.AddContactAsync(entity);
        }

        public async Task<IEnumerable<ContactResponseDto>> GetContactsAsync()
        {
            var entities = await repository.GetContactsAsync();
            return mapper.Map<IEnumerable<ContactResponseDto>>(entities);
        }

        public async Task<ContactResponseDto?> GetContactAsync(int id)
        {
            var entity = await repository.GetContactAsync(id);

            return entity is null
                ? throw new NotFoundException("Contact not found")
                : mapper.Map<ContactResponseDto>(entity);
        }

        public async Task<bool> UpdateContactAsync(UpdateContactDto dto)
        {
            var entity = await repository.GetContactAsync(dto.Id);

            if (entity is null)
                throw new NotFoundException("Contact not found");

            mapper.Map(dto, entity);

            return await repository.UpdateContactAsync(entity);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var deleted = await repository.DeleteContactAsync(id);

            return !deleted
                ? throw new NotFoundException("Contact not found")
                : true;
        }
    }
}