using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.GetToDoItem;

public class GetToDoItemEndpointTests : BaseTestFixture
{
    public GetToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {

    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsItem()
    {
        var data = Builder<GetToDoItemDto>.CreateNew().Build();

        _senderMock.Send(Arg.Any<GetToDoItemQuery>(), Arg.Any<CancellationToken>())
            .Returns(data);

        var sut = await GetToDoItemEndpoint.HandleAsync(
            1, 
            _senderMock,
            _mapper,
            CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Id.Should().Be(data.Id);
    }
}