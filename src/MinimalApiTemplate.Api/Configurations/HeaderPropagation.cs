using Microsoft.Extensions.Primitives;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Configurations;

public static class HeaderPropagation
{
    public static void ConfigureHeaderPropagation(this IServiceCollection services)
    {
        services.AddHeaderPropagation(options =>
            options.Headers.Add(Headers.CorrelationId, context =>
            {
                if (context.HttpContext.Request.Headers.TryGetValue(
                    Headers.CorrelationId, out StringValues requestCorrelationId)) return requestCorrelationId;

                if (context.HttpContext.Response.Headers.TryGetValue(
                    Headers.CorrelationId, out StringValues responseCorrelationId)) return responseCorrelationId;

                return new StringValues(Guid.NewGuid().ToString());
            }));
    }
}
