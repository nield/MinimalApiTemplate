using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApiTemplate.Api.Endpoints.V1.TodoItems;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems;

public class DeleteToDoItemEndpointTests : BaseTestFixture
{
    private readonly DeleteToDoItemEndpoint _endpoint;

    public DeleteToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock.Object, _mapper, _outputCacheStoreMock.Object);
    }

    [Fact]
    public async Task Handle_Given_ValidId_Then_ReturnsNoContent()
    {
        var sut = await _endpoint.HandleAsync(1, CancellationToken.None);

        sut.Should().BeOfType<NoContent>();

        _senderMock.Verify(x => x.Send(It.IsAny<DeleteTodoItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
