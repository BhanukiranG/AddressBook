using System.Text.Json.Serialization;

namespace AddressBook.Application.DTOs
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Successful { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
    }
}