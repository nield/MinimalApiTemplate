using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
using NSubstitute.ExceptionExtensions;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Queries.GetToDoItem;

public class GetToDoItemQueryTests : BaseTestFixture
{
    private readonly GetToDoItemQueryHandler _handler;

    public GetToDoItemQueryTests()
    {
        _handler = new GetToDoItemQueryHandler(_applicationDbContextMock);
    }

    [Fact]
    public void When_ToDoItemIdDoesNotExists_Then_ExceptionIsThrown()
    {
        var toDoItemsDbSetMock = new List<TodoItem>().AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.TodoItems
            .Returns(toDoItemsDbSetMock);

        _handler.Handle(new GetToDoItemQuery { Id = 1 }, CancellationToken.None)
            .Should().Throws<NotFoundException>();
    }

    [Fact]
    public async Task When_ToDoItemIdExists_Then_ReturnData()
    {
        var toDoItemsDbSetMock = Builder<TodoItem>.CreateListOfSize(1).Build()
                                    .AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.TodoItems
            .Returns(toDoItemsDbSetMock);

        var sut = await _handler.Handle(new GetToDoItemQuery { Id = 1 }, CancellationToken.None);

        sut.Should().NotBeNull();
    }
}