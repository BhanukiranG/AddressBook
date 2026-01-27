using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
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
            return Ok(result);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(_mapper.Map<ContactResponseDto>(contact));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            var id = await _repository.AddAsync(contact);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var contact = _mapper.Map<Contact>(dto);
            var updated = await _repository.UpdateAsync(contact);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
