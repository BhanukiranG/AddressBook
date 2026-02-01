using AddressBook.Application.DTOs.Contact;
using AddressBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController(IContactService contactService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ContactResponseDto>> GetContacts()
        {
            return await contactService.GetContactsAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ContactResponseDto?> GetContact(int id)
        {
            return await contactService.GetContactAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateContact(CreateContactDto dto)
        {
            return await contactService.CreateContactAsync(dto);
        }

        [HttpPatch("{id:int}")]
        public async Task<bool> UpdateContact(int id, UpdateContactDto dto)
        {
            dto.Id = id;
            return await contactService.UpdateContactAsync(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteContact(int id)
        {
            return await contactService.DeleteContactAsync(id);
        }
    }
}