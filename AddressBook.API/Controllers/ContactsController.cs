using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactRepository repository, IMapper mapper) : BaseApiController 
    {
        // GET: api/contacts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await repository.GetAllAsync();
            var result = mapper.Map<IEnumerable<ContactResponseDto>>(contacts);
            return Success(result);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await repository.GetByIdAsync(id);
            if (contact == null) return Failure("Contact not found", 404);

            var result = mapper.Map<ContactResponseDto>(contact);
            return Success(result);
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            var contact = mapper.Map<Contacts>(dto);
            var id = await repository.AddAsync(contact);

            var createdContact = await repository.GetByIdAsync(id);
            var result = mapper.Map<ContactResponseDto>(createdContact);

            return Success(result, 201);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto dto)
        {
            var contact = await repository.GetByIdAsync(id);
            if (contact == null) return Failure("Contact not found", 404);

            if (dto.Name != null && string.IsNullOrWhiteSpace(dto.Name)) return Failure("Name cannot be empty");
            if (dto.Email != null && !dto.Email.Contains('@')) return Failure("Invalid email address");
            if (dto.Phone is { Length: < 5 }) return Failure("Invalid phone number");

            if (dto.Name != null) contact.Name = dto.Name;
            if (dto.Email != null) contact.Email = dto.Email;
            if (dto.Phone != null) contact.Phone = dto.Phone;

            var updated = await repository.UpdateAsync(contact);
            if (!updated) return Failure("Could not update contact", 500);

            var result = mapper.Map<ContactResponseDto>(contact);
            return Success(result);
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await repository.GetByIdAsync(id);
            if (contact == null) return Failure("Contact not found", 404);

            var deleted = await repository.DeleteAsync(id);
            if (!deleted) return Failure("Could not delete contact", 500);

            return Success(mapper.Map<ContactResponseDto>(contact));
        }
    }
}
