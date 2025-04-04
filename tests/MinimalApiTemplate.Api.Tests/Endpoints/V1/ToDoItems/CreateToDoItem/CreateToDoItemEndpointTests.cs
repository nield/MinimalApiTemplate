using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.CreateToDoItem;

public class CreateToDoItemEndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Given_ValidModel_Then_ReturnsCreatedId()
    {
        var newId = 1L;
        var request = Builder<CreateTodoItemRequest>.CreateNew().Build();

        _senderMock.Send(Arg.Any<CreateTodoItemCommand>(), Arg.Any<CancellationToken>())
            .Returns(newId);

        var sut = await CreateToDoItemEndpoint.HandleAsync(
            request, 
            _senderMock,
            _outputCacheStoreMock,
            CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Id.Should().Be(newId);
    }
}
