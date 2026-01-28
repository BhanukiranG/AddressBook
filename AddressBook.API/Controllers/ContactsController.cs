using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactService service) : BaseApiController
    {
        // GET: api/contacts
        [HttpGet]
        public async Task<IActionResult> GetAll() => Success(await service.GetAllAsync());

        // GET: api/contacts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is null
                ? Failure("Contact not found", 404)
                : Success(result);
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDto dto)
        {
            var id = await service.CreateAsync(dto);
            var result = await service.GetByIdAsync(id);
            return Success(result, 201);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateContactDto dto)
        {
            dto.Id = id;
            var updated = await service.UpdateAsync(dto);
            return updated
                ? Success("Contact updated successfully")
                : Failure("Contact not found", 404);
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Success("Contact deleted successfully")
                : Failure("Contact not found", 404);
        }
    }
}