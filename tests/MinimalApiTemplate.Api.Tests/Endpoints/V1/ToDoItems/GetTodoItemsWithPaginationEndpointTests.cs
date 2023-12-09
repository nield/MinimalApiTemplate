using MinimalApiTemplate.Api.Endpoints.V1.TodoItems;
using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Application.Common.Models;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems;
public class GetTodoItemsWithPaginationEndpointTests : BaseTestFixture
{
    private readonly GetTodoItemsWithPaginationEndpoint _endpoint;

    public GetTodoItemsWithPaginationEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock, _mapper);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsItem()
    {
        var data = Builder<GetTodoItemsDto>.CreateListOfSize(1).Build();

        _senderMock.Send(Arg.Any<GetTodoItemsWithPaginationQuery>(), Arg.Any<CancellationToken>())
            .Returns(new PaginatedList<GetTodoItemsDto>(data, 10, 1, 10));

        var query = Builder<GetTodoItemsWithPaginationRequest>.CreateNew().Build();

        var sut = await _endpoint.HandleAsync(query, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Items.Should().HaveCount(1);
    }
}
