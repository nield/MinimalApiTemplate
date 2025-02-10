using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MinimalApiTemplate.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

        optionsBuilder.UseSqlServer(
            config.GetConnectionString("SqlDatabase"),
            builder =>
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    .MigrationsHistoryTable(ApplicationDbContext.MigrationTableName, ApplicationDbContext.DbSchema));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
