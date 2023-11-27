namespace MinimalApiTemplate.Infrastructure.Tests.Persistence;

public class PersistenceFixture
{
    public InMemoryApplicationDbContextFactory InMemoryApplicationDbContextFactory { get; set; }

    public PersistenceFixture()
    {
        InMemoryApplicationDbContextFactory = new InMemoryApplicationDbContextFactory();

        Console.WriteLine("Persistence setup done once");
    }
}