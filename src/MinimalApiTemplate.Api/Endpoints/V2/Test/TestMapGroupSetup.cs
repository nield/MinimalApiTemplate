using MinimalApiTemplate.Api.Endpoints.V1;

namespace MinimalApiTemplate.Api.Endpoints.V2.Test;
public static class TestMapGroupSetup
{
    public static RouteGroupBuilder TestRoute(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .RouteV2()
            .MapGroup("Test")
            .WithTags("Test");
}
