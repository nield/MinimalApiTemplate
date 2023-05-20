using MinimalApiTemplate.Api.Endpoints.V1.TodoItems;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems;

public class GetToDoItemEndpointTests : BaseTestFixture
{
    private readonly GetToDoItemEndpoint _endpoint;

    public GetToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsItem()
    {
        var data = Builder<GetToDoItemDto>.CreateNew().Build();

        _senderMock.Setup(x => x.Send(It.IsAny<GetToDoItemQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var sut = await _endpoint.HandleAsync(1, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Id.Should().Be(data.Id);
    }
}