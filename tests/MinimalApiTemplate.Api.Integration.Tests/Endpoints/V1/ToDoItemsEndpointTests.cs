using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Responses;

namespace MinimalApiTemplate.Api.Integration.Tests.Controllers.V1;

[Collection("WebApplicationCollection")]
public class ToDoItemsEndpointTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public ToDoItemsEndpointTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task Given_ValidData_When_CreatingTodoItem_Then_ReturnCreated()
    {
        var payload = Builder<CreateTodoItemRequest>.CreateNew().Build();

        var sut = await _webApplicationFixture.HttpClient.PostAsJsonAsync("/api/v1/todos", payload);

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Given_ExistingId_When_FetchingItem_Then_ReturnItem()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<GetToDoItemResponse>("/api/v1/todos/1");

        sut.Should().NotBeNull();
        sut!.Id.Should().Be(1);
    }

    [Fact]
    public async Task Given_NonExistingId_When_FetchingItem_Then_ReturnNotFound()
    {
        var sut = await _webApplicationFixture.HttpClient.GetAsync("/api/v1/todos/123");

        sut.Should().NotBeNull();

        sut.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Given_DataExists_When_FetchingItems_Then_ReturnPagedItem()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<PaginatedListResponse<GetToDoItemsResponse>>("/api/v1/todos?PageNumber=1&PageSize=10");

        sut.Should().NotBeNull();
        sut!.Items.Count.Should().NotBe(0);
        sut.PageNumber.Should().Be(1);
        sut.PageSize.Should().Be(10);
        sut.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Given_ExistingId_When_DeletingItem_Then_ReturnNoContent()
    {
        var sut = await _webApplicationFixture.HttpClient.DeleteAsync("/api/v1/todos/1");

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await _webApplicationFixture.ResetDatabaseAsync();
    }

    [Fact]
    public async Task Given_NonExistingId_When_DeletingItem_Then_ReturnNotFound()
    {
        var sut = await _webApplicationFixture.HttpClient.DeleteAsync("/api/v1/todos/123");

        sut.Should().NotBeNull();
        sut.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
