using PetaPoco;

namespace AddressBook.Infrastructure.Configurations
{
    public interface IDatabaseFactory
    {
        IDatabase GetDatabase();
    }
}