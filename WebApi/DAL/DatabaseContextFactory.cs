using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApi.DAL
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();

            string connectionString = config.GetConnectionString("DBConnectionString") ?? string.Empty;
            optionsBuilder.UseSqlServer(connectionString);

            DatabaseContext context = new(optionsBuilder.Options);

            DatabaseInitializer.Initialize(context);

            return context;
        }
    }
}