namespace MinimalApiTemplate.Api.Integration.Tests.Endpoints.V1.ToDoItems.DeleteToDoItem;

[Collection("WebApplicationCollection")]
public class EndpointTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public EndpointTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task Given_ExistingId_When_DeletingItem_Then_ReturnNoContent()
    {
        var sut = await _webApplicationFixture.HttpClient.DeleteAsync(
            "/api/v1/todos/1",
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await _webApplicationFixture.ResetDatabaseAsync();
    }

    [Fact]
    public async Task Given_NonExistingId_When_DeletingItem_Then_ReturnNotFound()
    {
        var sut = await _webApplicationFixture.HttpClient.DeleteAsync(
            "/api/v1/todos/123",
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
