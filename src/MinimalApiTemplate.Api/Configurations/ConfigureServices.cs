using System.Diagnostics;
using MinimalApiTemplate.Api.Common.Services;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Configurations;

public static class ConfigureServices
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureAuth(config);

        builder.Services.ConfigureFluentValidator();

        builder.Services.ConfigureExceptionHandlers();

        builder.Services.AddApiEndpoints();

        builder.Services.ConfigureSwagger(config);

        builder.Services.ConfigureVersioning();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureCompression();

        builder.Services.ConfigureHeaderPropagation();

        builder.Services.AddProblemDetails(options => 
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
       
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                context.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.TraceId);
            });

        return builder;
    }
}
