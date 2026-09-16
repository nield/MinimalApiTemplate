using MinimalApiTemplate.Application.Common.Models;

namespace MinimalApiTemplate.Api.Common.Models;

public static class PagedListMapper
{
    public static PaginatedListResponse<TDest> MapToPaginatedList<TSource, TDest>(
        this PaginatedList<TSource> source,
        Func<TSource, TDest> mapItem)
    {
        return new PaginatedListResponse<TDest>
        {
            Items = source.Items.Select(mapItem).ToList(),
            PageNumber = source.PageNumber,
            TotalPages = source.TotalPages,
            TotalCount = source.TotalCount,
            PageSize = source.PageSize,
            HasPreviousPage = source.HasPreviousPage,
            HasNextPage = source.HasNextPage
        };    
    }
}
