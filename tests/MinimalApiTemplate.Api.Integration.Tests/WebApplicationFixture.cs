using System.Net.Http.Headers;
using Respawn;

namespace MinimalApiTemplate.Api.Integration.Tests;
public  class WebApplicationFixture
{
    private readonly CustomWebApplicationFactory<global::Program> _factory;

    public HttpClient HtppClient { get; }
    public Respawner Respawner { get; }

    public WebApplicationFixture()
    {
        _factory = new();

        HtppClient = _factory.CreateClient();
        HtppClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        Respawner = Respawner.CreateAsync(Environment.DatabaseConnectionString).GetAwaiter().GetResult();
    }
}
