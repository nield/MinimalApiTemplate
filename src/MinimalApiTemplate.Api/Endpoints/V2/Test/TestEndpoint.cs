namespace MinimalApiTemplate.Api.Endpoints.V2.Test;

public class TestEndpoint : IEndpoint<IResult>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.TestRoute()
            .MapGet("", () => HandleAsync());
    }

    public Task<IResult> HandleAsync()
    {
        throw new NotImplementedException();
    }
}
