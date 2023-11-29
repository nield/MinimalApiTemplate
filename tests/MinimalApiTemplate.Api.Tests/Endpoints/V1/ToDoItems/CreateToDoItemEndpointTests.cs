using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApiTemplate.Api.Endpoints.V1.TodoItems;
using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems;

public class CreateToDoItemEndpointTests : BaseTestFixture
{
    private readonly CreateToDoItemEndpoint _endpoint;

    public CreateToDoItemEndpointTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _endpoint = new(_senderMock.Object, _mapper, _outputCacheStoreMock.Object);
    }

    [Fact]
    public async Task Handle_Given_ValidModel_Then_ReturnsCreatedId()
    {
        var newId = 1L;
        var request = Builder<CreateTodoItemRequest>.CreateNew().Build();

        _senderMock.Setup(x => x.Send(It.IsAny<CreateTodoItemCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(newId);

        var sut = await _endpoint.HandleAsync(request, CancellationToken.None);
        sut.Should().NotBeNull();        
        
        var result = sut.As<CreatedAtRoute<CreateTodoItemResponse>>();
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value!.Id.Should().Be(newId);
    }
}
