using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;
using MinimalApiTemplate.Application.Common.Models;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.GetTodoItemsWithPagination;
public class GetTodoItemsWithPaginationEndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsItem()
    {
        var data = Builder<GetTodoItemsDto>.CreateListOfSize(1).Build();

        _senderMock.Send(Arg.Any<GetTodoItemsWithPaginationQuery>(), Arg.Any<CancellationToken>())
            .Returns(new PaginatedList<GetTodoItemsDto>(data, 10, 1, 10));

        var query = Builder<GetTodoItemsWithPaginationRequest>.CreateNew().Build();

        var sut = await GetTodoItemsWithPaginationEndpoint.HandleAsync(
            query, 
            _senderMock,
            CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Items.Should().HaveCount(1);
    }
}
