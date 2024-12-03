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

        builder.Services.ConfigureFluentValidator();

        builder.Services.ConfigureAutoMapper();

        builder.Services.ConfigureExceptionHandlers();

        builder.Services.AddApiEndpoints();

        builder.Services.ConfigureSwagger();

        builder.Services.ConfigureVersioning();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureCompression();

        builder.Services.ConfigureHeaderPropagation();

        return builder;
    }
}
