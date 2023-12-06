using MinimalApiTemplate.Api.Endpoints.V1.TodoItems;
using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems;
public class UpdateToDoItemEndpointTests : BaseTestFixture
{
    private readonly UpdateToDoItemEndpoint _endpoint;

    public UpdateToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock.Object, _mapper, _outputCacheStoreMock.Object);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsNoContent()
    {
        var request = Builder<UpdateTodoItemRequest>.CreateNew().Build();

        var sut = await _endpoint.HandleAsync(1, request, CancellationToken.None);

        sut.Should().NotBeNull();

        _senderMock.Verify(x => x.Send(It.IsAny<UpdateTodoItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
