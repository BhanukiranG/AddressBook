using AddressBook.Application.DTOs;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : BaseApiController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ContactResponseDto>>>> GetContacts()
        {
            var result = await this._contactService.GetAllAsync();
            return Success(result);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<ContactResponseDto>>> GetContact(int id)
        {
            var result = await this._contactService.GetByIdAsync(id);
            return result is null
                ? Failure<ContactResponseDto>("Contact not found", 404)
                : Success(result);
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ContactResponseDto?>>> CreateContact(CreateContactDto dto)
        {
            var id = await this._contactService.CreateAsync(dto);
            var result = await this._contactService.GetByIdAsync(id);
            return Success(result, 201);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateContact(int id, UpdateContactDto dto)
        {
            dto.Id = id;
            var updated = await this._contactService.UpdateAsync(dto);
            return updated
                ? Success("Contact updated successfully")
                : Failure<string>("Contact not found", 404);
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteContact(int id)
        {
            var deleted = await this._contactService.DeleteAsync(id);
            return deleted
                ? Success("Contact deleted successfully")
                : Failure<string>("Contact not found", 404);
        }
    }
}