namespace AddressBook.Application.DTOs
{
    public class ApiResponse<T>
    {
        public required T Data { get; set; }
        public bool Successful { get; set; }
    }

    public class ApiErrorResponse
    {
        public required string Message { get; set; }
        public bool Successful { get; set; } = false;
    }
}