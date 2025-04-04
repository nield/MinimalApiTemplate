using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

[Mapper]
public static partial class GetToDoItemMapper
{
    public static partial GetToDoItemResponse MapToResponse(this GetToDoItemDto source);
}
