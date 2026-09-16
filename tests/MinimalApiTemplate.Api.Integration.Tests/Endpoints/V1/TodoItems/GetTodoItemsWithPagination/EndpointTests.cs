using MinimalApiTemplate.Api.Common.Models;
using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Integration.Tests.Endpoints.V1.ToDoItems.GetTodoItemsWithPagination;

[Collection("WebApplicationCollection")]
public class EndpointTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public EndpointTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task Given_DataExists_When_FetchingItems_Then_ReturnPagedItem()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<PaginatedListResponse<Response>>(
            "/api/v1/todos?PageNumber=1&PageSize=10",
            cancellationToken: TestContext.Current.CancellationToken);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().NotBe(0);
        sut.PageNumber.Should().Be(1);
        sut.PageSize.Should().Be(10);
        sut.TotalPages.Should().Be(1);
    }
}
