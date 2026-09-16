using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.CreateToDoItem;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Given_ValidModel_Then_ReturnsCreatedId()
    {
        var newId = 1L;
        var request = Builder<Request>.CreateNew().Build();

        _senderMock.Send(Arg.Any<CreateTodoItemCommand>(), Arg.Any<CancellationToken>())
            .Returns(newId);

        var sut = await Endpoint.HandleAsync(
            request, 
            _senderMock,
            _outputCacheStoreMock,
            CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Value.Should().NotBeNull();
        sut.Value!.Id.Should().Be(newId);
    }
}
