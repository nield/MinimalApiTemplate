using MinimalApiTemplate.Api.ExceptionHandlers;

namespace MinimalApiTemplate.Api.Configurations;

public static class ExceptionHandlers
{
    public static void ConfigureExceptionHandlers(this IServiceCollection services)
    {        
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
        services.AddExceptionHandler<ForbiddenAccessExceptionHandler>();

    }
}