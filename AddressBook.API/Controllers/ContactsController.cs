using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactService contactService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ContactResponseDto>> GetContacts()
        {
            return await contactService.GetAllAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ContactResponseDto?> GetContact(int id)
        {
            return await contactService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateContact(CreateContactDto dto)
        {
            return await contactService.CreateAsync(dto);
        }

        [HttpPatch("{id:int}")]
        public async Task<bool> UpdateContact(int id, UpdateContactDto dto)
        {
            dto.Id = id;
            return await contactService.UpdateAsync(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteContact(int id)
        {
            return await contactService.DeleteAsync(id);
        }
    }
}