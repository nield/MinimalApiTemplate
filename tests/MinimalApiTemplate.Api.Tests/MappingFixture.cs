using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTemplate.Api.Tests;

public class MappingFixture
{
    public IMapper Mapper { get; set; }

    public MappingFixture()
    {
        var services = new ServiceCollection();

        services.AddAutoMapper(typeof(Program).Assembly);

        var serviceProvider = services.BuildServiceProvider();

        Mapper = serviceProvider.GetRequiredService<IMapper>();

        Console.WriteLine("Mappings setup done once");
    }
}
