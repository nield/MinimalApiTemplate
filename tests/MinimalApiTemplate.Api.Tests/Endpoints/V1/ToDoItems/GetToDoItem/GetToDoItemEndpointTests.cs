using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.GetToDoItem;

public class GetToDoItemEndpointTests : BaseTestFixture
{
    private readonly GetToDoItemEndpoint _endpoint;

    public GetToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock, _mapper);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsItem()
    {
        var data = Builder<GetToDoItemDto>.CreateNew().Build();

        _senderMock.Send(Arg.Any<GetToDoItemQuery>(), Arg.Any<CancellationToken>())
            .Returns(data);

        var sut = await _endpoint.HandleAsync(1, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Id.Should().Be(data.Id);
    }
}