using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

[Mapper]
public static partial class Mapper
{
    public static partial Response MapToResponse(this GetToDoItemDto source);
}
