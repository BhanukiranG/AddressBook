namespace AddressBook.Application.DTOs
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Successful { get; set; }
        public string Message { get; set; } = "";
    }
}