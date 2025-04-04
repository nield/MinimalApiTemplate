namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class GetTodoItemsMapper
{
    public static partial IQueryable<GetTodoItemsDto> ProjectToDto(this IQueryable<TodoItem> source);
}
