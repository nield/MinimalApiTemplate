using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

public class GetToDoItemQuery : IRequest<GetToDoItemDto>
{
    public long Id { get; set; }
}

public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, GetToDoItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetToDoItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetToDoItemDto> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TodoItems
                            .AsNoTracking()
                            .Where(x => x.Id == request.Id)
                            .OrderBy(x => x.Title)
                            .ProjectTo<GetToDoItemDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return data ?? throw new NotFoundException(nameof(TodoItem), request.Id);
    }
}
