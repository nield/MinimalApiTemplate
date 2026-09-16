using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

namespace MinimalApiTemplate.Api.Integration.Tests.Endpoints.V1.ToDoItems.GetToDoItem;

[Collection("WebApplicationCollection")]
public class EndpointTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public EndpointTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task Given_ExistingId_When_FetchingItem_Then_ReturnItem()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<Response>(
            "/api/v1/todos/1",
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
        sut.Id.Should().Be(1);
    }

    [Fact]
    public async Task Given_NonExistingId_When_FetchingItem_Then_ReturnNotFound()
    {
        var sut = await _webApplicationFixture.HttpClient.GetAsync(
            "/api/v1/todos/123",
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();

        sut.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
