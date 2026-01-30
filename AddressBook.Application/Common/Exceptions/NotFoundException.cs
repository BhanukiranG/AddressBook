namespace AddressBook.Application.Common.Exceptions
{
    public class NotFoundException(string message) : Exception(message);
}