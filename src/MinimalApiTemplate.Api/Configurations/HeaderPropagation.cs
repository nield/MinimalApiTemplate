using MinimalApiTemplate.Api.Common.Extensions;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Configurations;

public static class HeaderPropagation
{
    public static void ConfigureHeaderPropagation(this IServiceCollection services)
    {
        services.AddHeaderPropagation(options =>
            options.Headers.Add(Headers.CorrelationId, context => context.HttpContext.GetCorrelationId()));
    }
}
