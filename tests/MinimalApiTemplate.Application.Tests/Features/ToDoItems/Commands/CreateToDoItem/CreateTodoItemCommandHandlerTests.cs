using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Commands.CreateToDoItem;

public class CreateToDoItemCommandHandlerTests : BaseTestFixture<CreateTodoItemCommandHandler>
{
    private readonly CreateTodoItemCommandHandler _handler;

    public CreateToDoItemCommandHandlerTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _handler = new(_templateRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Given_ValidModel_Should_SaveData()
    {
        var toDoItem = Builder<CreateTodoItemCommand>.CreateNew().Build();

        await _handler.Handle(toDoItem, CancellationToken.None);

        _templateRepositoryMock.Verify(x => x.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
