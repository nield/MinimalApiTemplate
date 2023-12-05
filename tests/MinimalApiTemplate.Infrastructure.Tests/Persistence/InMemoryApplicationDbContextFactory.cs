using MinimalApiTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MinimalApiTemplate.Domain.Entities;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence;

public static class InMemoryApplicationDbContextFactory
{    
    public static ApplicationDbContext CreateContext(string databaseName)
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>(
            new DbContextOptions<ApplicationDbContext>())
                .UseInMemoryDatabase(databaseName)
                .Options;

        var dbContext = new ApplicationDbContext(contextOptions);

        SeedData(dbContext);

        return dbContext;
    }

    private static void SeedData(ApplicationDbContext dbContext)
    {
        // Ensure existing seed data is saved before adding new data.
        dbContext.Database.EnsureCreated();

        dbContext.TodoItems.Add(new TodoItem { Id = 2, Title = "Testing", IsDeleted = false });

        dbContext.SaveChanges();
    }
}