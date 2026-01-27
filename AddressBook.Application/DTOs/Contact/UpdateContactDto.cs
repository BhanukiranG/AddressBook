namespace AddressBook.Application.DTOs.Contact
{
    public class UpdateContactDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}