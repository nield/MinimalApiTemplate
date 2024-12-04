namespace MinimalApiTemplate.Api.Common.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app)
    {
        foreach (IEndpoint service in app.Services.GetServices<IEndpoint>())
        {
            service.AddRoute(app);
        }

        return app;
    }
}
