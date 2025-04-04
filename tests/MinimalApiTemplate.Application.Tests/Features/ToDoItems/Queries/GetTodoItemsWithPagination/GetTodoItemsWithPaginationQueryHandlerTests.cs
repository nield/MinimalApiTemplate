using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationQueryHandlerTests : BaseTestFixture
{
    private readonly GetTodoItemsWithPaginationQueryHandler _handler;

    public GetTodoItemsWithPaginationQueryHandlerTests()
    {
        _handler = new(_applicationDbContextMock);
    }

    [Fact]
    public async Task Given_ToDoItemIdExists_Then_ReturnData()
    {
        var toDoItemsDbSetMock = Builder<TodoItem>.CreateListOfSize(1).Build()
                                    .AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.TodoItems
            .Returns(toDoItemsDbSetMock);

        var sut = await _handler.Handle(new GetTodoItemsWithPaginationQuery(), CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(1);
    }
}
