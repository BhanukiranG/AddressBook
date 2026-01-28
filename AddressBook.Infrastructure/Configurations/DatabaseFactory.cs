using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using System.Data.Common;

namespace AddressBook.Infrastructure.Configurations
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly string _connectionString;

        public DatabaseFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;

            // Register the Microsoft.Data.SqlClient factory with PetaPoco
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
        }

        public IDatabase GetDatabase()
        {
            return new Database(
                _connectionString,
                "Microsoft.Data.SqlClient"
            );
        }
    }
}