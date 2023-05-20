using MinimalApiTemplate.Application.Common.Models;

namespace MinimalApiTemplate.Api.Models;

public class PagedListProfile : Profile
{
    public PagedListProfile()
    {
        CreateMap(typeof(PaginatedList<>), typeof(PaginatedListResponse<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}

public class PagedListConverter<TSource, TDest>
    : ITypeConverter<PaginatedList<TSource>, PaginatedListResponse<TDest>>
{
    public PaginatedListResponse<TDest> Convert(PaginatedList<TSource> source, PaginatedListResponse<TDest> destination, ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDest>>(source.Items);

        return new PaginatedListResponse<TDest>
        {
            Items = mappedItems,
            PageSize = source.PageSize,
            PageNumber = source.PageNumber,
            TotalCount = source.TotalCount,
            TotalPages = source.TotalPages,
            HasNextPage = source.HasNextPage,
            HasPreviousPage = source.HasPreviousPage
        };
    }
}
