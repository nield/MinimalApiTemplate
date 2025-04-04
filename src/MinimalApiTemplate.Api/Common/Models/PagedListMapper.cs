using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;
using MinimalApiTemplate.Application.Common.Models;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Common.Models;

[Mapper]
public static partial class PagedListMapper
{
    public static partial TDest MapItem<TSource, TDest>(TSource source);

    public static PaginatedListResponse<TDest> MapToPaginatedList<TSource, TDest>(this PaginatedList<TSource> source) 
    {
        return new PaginatedListResponse<TDest>
        {
            Items = source.Items.Select(x => MapItem<TSource, TDest>(x)).ToList(),
            PageNumber = source.PageNumber,
            TotalPages = source.TotalPages,
            TotalCount = source.TotalCount,
            PageSize = source.PageSize,
            HasPreviousPage = source.HasPreviousPage,
            HasNextPage = source.HasNextPage
        };    
    }

    private static partial GetToDoItemsResponse MapGetToDoItemsResponse(GetTodoItemsDto source);
}
