using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.UpdateToDoItem;

public class UpdateToDoItemEndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsNoContent()
    {
        var request = Builder<UpdateTodoItemRequest>.CreateNew().Build();

        var sut = await UpdateToDoItemEndpoint.HandleAsync(
            1, 
            request,
            _senderMock,
            _outputCacheStoreMock,
            CancellationToken.None);

        sut.Should().NotBeNull();

        await _senderMock.Received(1)
            .Send(Arg.Any<UpdateTodoItemCommand>(), Arg.Any<CancellationToken>());
    }
}
