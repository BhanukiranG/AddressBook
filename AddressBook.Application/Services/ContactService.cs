using AutoMapper;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateContactDto dto)
        {
            var entity = _mapper.Map<Contacts>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactResponseDto>>(entities);
        }

        public async Task<ContactResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactResponseDto?>(entity);
        }

        public async Task<bool> UpdateAsync(UpdateContactDto dto)
        {
            var entity = _mapper.Map<Contacts>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}