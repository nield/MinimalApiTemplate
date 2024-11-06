using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MinimalApiTemplate.Infrastructure.Persistence;

// Temporary until docker in docker works on build agent
[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHostEnvironment _hostEnvironment;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        IHostEnvironment hostEnvironment,
        ILogger<ApplicationDbContextInitialiser> logger
    )
    {
        _logger = logger;
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task MigrateDatabaseAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Ensure existing seed data is saved before adding new data.
        await _context.Database.EnsureCreatedAsync();

        if (_hostEnvironment.IsTest())
        {
            _context.TodoItems.Add(new TodoItem { Title = "Testing Item Init", IsDeleted = false });
        }

        await _context.SaveChangesAsync();
    }
}
