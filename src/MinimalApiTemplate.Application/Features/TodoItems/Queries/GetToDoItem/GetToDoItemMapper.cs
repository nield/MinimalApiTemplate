namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class GetToDoItemMapper 
{
    public static partial IQueryable<GetToDoItemDto> ProjectToDto(this IQueryable<TodoItem> source);
}
