using System.Reflection;
using Microsoft.Extensions.Hosting;
using MinimalApiTemplate.Application.Common.Behaviours;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return builder;
    }
}
