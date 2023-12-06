using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandHandlerTests : BaseTestFixture<UpdateTodoItemCommandHandler>
{
    private readonly UpdateTodoItemCommandHandler _handler;

    public UpdateTodoItemCommandHandlerTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _handler = new(_templateRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Given_IdDoesNotExists_Then_ThrowAnException()
    {
        var id = 1L;

        _templateRepositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
                    _handler.Handle(Builder<UpdateTodoItemCommand>.CreateNew()
                                        .With(x => id)
                                        .Build(), CancellationToken.None));

        _templateRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Given_IdDoesExists_Then_UpdateAndSave()
    {
        var id = 1L;

        _templateRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Builder<TodoItem>.CreateNew().Build());

        await _handler.Handle(Builder<UpdateTodoItemCommand>.CreateNew()
                                        .With(x => id)
                                        .Build(), CancellationToken.None);

        _templateRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
