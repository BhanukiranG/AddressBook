namespace AddressBook.Domain.Entities
{
    public class Contacts
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}