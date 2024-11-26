using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.DeleteToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.DeleteToDoItem;

public class DeleteToDoItemEndpointTests : BaseTestFixture
{
    private readonly DeleteToDoItemEndpoint _endpoint;

    public DeleteToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock, _mapper, _outputCacheStoreMock);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsNoContent()
    {
        var sut = await _endpoint.HandleAsync(1, CancellationToken.None);

        sut.Should().NotBeNull();

        await _senderMock.Received(1)
            .Send(Arg.Any<DeleteTodoItemCommand>(), Arg.Any<CancellationToken>());
    }
}
