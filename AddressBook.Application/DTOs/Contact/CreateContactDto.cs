namespace AddressBook.Application.DTOs.Contact
{
    public class CreateContactDto
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}