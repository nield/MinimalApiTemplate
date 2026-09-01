namespace MinimalApiTemplate.Api.Common.Interfaces;

public interface IEndpoint
{
    static abstract void AddRoute(IEndpointRouteBuilder app);
}
