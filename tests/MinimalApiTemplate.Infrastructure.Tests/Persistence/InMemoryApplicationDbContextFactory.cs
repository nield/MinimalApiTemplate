using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;
using MinimalApiTemplate.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiTemplate.Domain.Entities;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence;

public class InMemoryApplicationDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryApplicationDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ApplicationDbContext CreateContext(string databaseName)
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>())
                .UseInMemoryDatabase(databaseName)
                .Options;

        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var auditInterceptor = _serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();

        var dbContext = new ApplicationDbContext(contextOptions, mediator, auditInterceptor);

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