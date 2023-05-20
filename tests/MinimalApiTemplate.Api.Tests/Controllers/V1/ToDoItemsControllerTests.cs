//using MinimalApiTemplate.Api.Controllers.V1;
//using MinimalApiTemplate.Api.Models.V1.Requests;
//using MinimalApiTemplate.Api.Models.V1.Responses;
//using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
//using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

//namespace MinimalApiTemplate.Api.Tests.Controllers.V1;

//public class ToDoItemsControllerTests : BaseTestFixture
//{
//    private readonly TodoItemsController _controller;

//    public ToDoItemsControllerTests(MappingFixture mappingFixture) 
//        : base(mappingFixture)
//    {
//        _controller = new TodoItemsController(_senderMock.Object, _mapper);
//    }

//    [Fact]
//    public async Task Create_Given_ValidModel_Then_ReturnsCreatedId()
//    {
//        var newId = 1L;
//        var request = Builder<CreateTodoItemRequest>.CreateNew().Build();

//        _senderMock.Setup(x => x.Send(It.IsAny<CreateTodoItemCommand>(), It.IsAny<CancellationToken>()))
//            .ReturnsAsync(newId);

//        var sut = await _controller.Create(request, CancellationToken.None);
//        sut.Should().NotBeNull();

//        var result = sut.As<CreatedAtRouteResult>();
//        result.Should().NotBeNull();

//        var value = result.Value.As<CreateTodoItemResponse>();
//        value.Should().NotBeNull();

//        value.Id.Should().Be(newId);
//    }

//    [Fact]
//    public async Task Get_Given_ValidId_Then_ReturnsItem()
//    {
//        var data = Builder<GetToDoItemDto>.CreateNew().Build();

//        _senderMock.Setup(x => x.Send(It.IsAny<GetToDoItemQuery>(), It.IsAny<CancellationToken>()))
//            .ReturnsAsync(data);

//        var sut = await _controller.Get(1, CancellationToken.None);

//        sut.Value.Should().NotBeNull();
//        sut.Value!.Id.Should().Be(data.Id);
//    }

//    [Fact]
//    public async Task Update_Given_ValidId_Then_ReturnsNoContent()
//    {
//        var request = Builder<UpdateTodoItemRequest>.CreateNew().Build();

//        var sut = await _controller.Update(1, request, CancellationToken.None);

//        sut.Should().BeOfType<NoContentResult>();
//    }

//    [Fact]
//    public async Task Delete_Given_ValidId_Then_ReturnsNoContent()
//    {
//        var sut = await _controller.Delete(1, CancellationToken.None);

//        sut.Should().BeOfType<NoContentResult>();
//    }
//}
