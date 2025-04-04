using Microsoft.EntityFrameworkCore;
using MinimalApiTemplate.Application.Common.Models;

namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<GetTodoItemsDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string[] Tags { get; set; } = [];
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<GetTodoItemsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<GetTodoItemsDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var querableToDoItems = _context.TodoItems.AsNoTracking();

        if (request.Tags.Length != 0)
        {
            querableToDoItems = querableToDoItems.Where(x => 
                request.Tags.Any(requestTag => x.Tags.Contains(requestTag)));
        }

        var pagedData = await querableToDoItems.OrderBy(x => x.Title)
            .ProjectToDto()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return pagedData;
    }
}
