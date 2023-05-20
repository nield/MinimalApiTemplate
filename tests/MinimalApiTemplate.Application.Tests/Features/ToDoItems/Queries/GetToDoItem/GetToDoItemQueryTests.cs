using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Queries.GetToDoItem;

public class GetToDoItemQueryTests : BaseTestFixture
{
    private readonly GetToDoItemQueryHandler _handler;

    public GetToDoItemQueryTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _handler = new GetToDoItemQueryHandler(_applicationDbContextMock.Object, _mapper);
    }

    [Fact]
    public async Task When_ToDoItemIdDoesNotExists_Then_ExceptionIsThrown()
    {
        var toDoItemsDbSetMock = new List<TodoItem>().AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.TodoItems)
            .Returns(toDoItemsDbSetMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetToDoItemQuery { Id = 1 }, CancellationToken.None));
    }

    [Fact]
    public async Task When_ToDoItemIdExists_Then_ReturnData()
    {
        var toDoItemsDbSetMock = Builder<TodoItem>.CreateListOfSize(1).Build()
                                    .AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.TodoItems)
            .Returns(toDoItemsDbSetMock.Object);

        var sut = await _handler.Handle(new GetToDoItemQuery { Id = 1 }, CancellationToken.None);

        sut.Should().NotBeNull();
    }
}