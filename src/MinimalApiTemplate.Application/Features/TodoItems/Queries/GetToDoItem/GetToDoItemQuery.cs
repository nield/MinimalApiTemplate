using Microsoft.EntityFrameworkCore;

namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

public class GetToDoItemQuery : IRequest<GetToDoItemDto>
{
    public long Id { get; set; }
}

public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, GetToDoItemDto>
{
    private readonly IApplicationDbContext _context;

    public GetToDoItemQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<GetToDoItemDto> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TodoItems
                    .AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .OrderBy(x => x.Title)
                    .ProjectToDto()
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);    

        return data ?? throw new NotFoundException(nameof(TodoItem), request.Id);
    }
}
