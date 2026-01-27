using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : BaseApiController
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactsController(
            IContactRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ContactResponseDto>>(contacts);
            return Success(result);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return Failure("Contact not found", 404);

            var result = _mapper.Map<ContactResponseDto>(contact);
            return Success(result);
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            var contact = _mapper.Map<Contacts>(dto);
            var id = await _repository.AddAsync(contact);

            var createdContact = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<ContactResponseDto>(createdContact);

            return Success(result, 201); // 201 Created
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto dto)
        {
            if (id != dto.Id) return Failure("ID mismatch", 400);

            var contact = _mapper.Map<Contacts>(dto);
            var updated = await _repository.UpdateAsync(contact);

            if (!updated) return Failure("Contact not found", 404);

            var updatedContact = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<ContactResponseDto>(updatedContact);

            return Success(result);
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return Failure("Contact not found", 404);

            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return Failure("Could not delete contact", 500);

            return Success(_mapper.Map<ContactResponseDto>(contact));
        }
    }
}
