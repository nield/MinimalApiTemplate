using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using NSubstitute.ReturnsExtensions;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandHandlerTests : BaseTestFixture<UpdateTodoItemCommandHandler>
{
    private readonly UpdateTodoItemCommandHandler _handler;

    public UpdateTodoItemCommandHandlerTests()
    {
        _handler = new(_templateRepositoryMock);
    }

    [Fact]
    public async Task Given_IdDoesNotExists_Then_ThrowAnException()
    {
        var id = 1L;

        _templateRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        var command = Builder<UpdateTodoItemCommand>.CreateNew()
                                        .With(x => id)
                                        .Build();

        await Assert.ThrowsAsync<NotFoundException>(() =>
                    _handler.Handle(command, CancellationToken.None).AsTask());

        await _templateRepositoryMock.DidNotReceive()
            .UpdateAsync(Arg.Any<TodoItem>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Given_IdDoesExists_Then_UpdateAndSave()
    {
        var id = 1L;

        _templateRepositoryMock.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(Builder<TodoItem>.CreateNew().Build());

        await _handler.Handle(Builder<UpdateTodoItemCommand>.CreateNew()
                                        .With(x => id)
                                        .Build(), CancellationToken.None);

        await _templateRepositoryMock.Received()
            .UpdateAsync(Arg.Any<TodoItem>(), Arg.Any<CancellationToken>());
    }
}
