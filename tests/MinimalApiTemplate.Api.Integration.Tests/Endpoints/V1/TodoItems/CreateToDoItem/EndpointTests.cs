using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

namespace MinimalApiTemplate.Api.Integration.Tests.Endpoints.V1.ToDoItems.CreateToDoItem;

[Collection("WebApplicationCollection")]
public class EndpointTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public EndpointTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task Given_ValidData_When_CreatingTodoItem_Then_ReturnCreated()
    {
        var payload = Builder<Request>.CreateNew().Build();

        var sut = await _webApplicationFixture.HttpClient.PostAsJsonAsync(
            "/api/v1/todos", 
            payload,
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
