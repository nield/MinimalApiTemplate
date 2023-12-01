using Microsoft.Extensions.Primitives;

namespace MinimalApiTemplate.Api.Configurations;

public static class HeaderPropagation
{
    private const string CorrelationIdHeader = "x-correlation-id";

    public static void ConfigureHeaderPropagation(this IServiceCollection services)
    {
        services.AddHeaderPropagation(options =>
            options.Headers.Add(CorrelationIdHeader, context =>
            {
                if (context.HttpContext.Request.Headers.TryGetValue(
                    CorrelationIdHeader, out StringValues requestCorrelationId)) return requestCorrelationId;

                if (context.HttpContext.Response.Headers.TryGetValue(
                    CorrelationIdHeader, out StringValues responseCorrelationId)) return responseCorrelationId;

                return new StringValues(Guid.NewGuid().ToString());
            }));
    }
}
