using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MinimalApiTemplate.Api.Integration.Tests.Containers;
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
    public async Task CreateTodoItem_Success()
    {
        var payload = Builder<CreateTodoItemRequest>.CreateNew().Build();

        var sut = await _webApplicationFixture.HttpClient.PostAsync(
            "/api/v1/todos",
            new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        );

        sut.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Given_ExistingId_When_FetchingItem_Then_ReturnItem()
    {
        var response = await _webApplicationFixture.HttpClient.GetAsync("/api/v1/todos/1");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<GetToDoItemResponse>();

        data.Should().NotBeNull();
        data!.Id.Should().Be(1);
    }

    [Fact]
    public async Task When_FetchingItems_Then_ReturnPagedItem()
    {
        var response = await _webApplicationFixture.HttpClient.GetAsync("/api/v1/todos?PageNumber=1&PageSize=10");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<PaginatedListResponse<GetToDoItemsResponse>>();

        data.Should().NotBeNull();
        data!.Items.Count.Should().NotBe(0);
        data.PageNumber.Should().Be(1);
        data.PageSize.Should().Be(10);
        data.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Given_ExistingId_When_DeletingItem_Then_ReturnNoContent()
    {
        var response = await _webApplicationFixture.HttpClient.DeleteAsync("/api/v1/todos/1");        

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await _webApplicationFixture.Respawner.ResetAsync(DatabaseContainer.Instance.GetConnectionString());
    }
}
