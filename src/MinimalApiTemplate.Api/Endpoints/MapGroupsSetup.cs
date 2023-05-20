using MinimalApiTemplate.Api.Endpoints.Validation;

namespace MinimalApiTemplate.Api.Endpoints.V1;

public static class MapGroupsSetup
{
    public static RouteGroupBuilder Route(this IEndpointRouteBuilder webApplication) =>
        webApplication.MapGroup("api/v{version:apiVersion}")
            .WithOpenApi()
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
}
