using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V2.ToDoItems.GetToDoItem;

[Mapper( RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class Mapper
{
    public static partial Response MapGetToDoItemResponse(this GetToDoItemDto source);
}
