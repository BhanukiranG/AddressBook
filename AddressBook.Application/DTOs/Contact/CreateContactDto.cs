using System.ComponentModel.DataAnnotations;

namespace AddressBook.Application.DTOs.Contact
{
    public class CreateContactDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(1, ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
    }
}