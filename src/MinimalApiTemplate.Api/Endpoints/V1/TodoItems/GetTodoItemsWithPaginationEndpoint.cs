﻿using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class GetTodoItemsWithPaginationEndpoint : BaseEndpoint, 
    IEndpoint<PaginatedListResponse<GetToDoItemsResponse>, GetTodoItemsWithPaginationQuery, CancellationToken>
{
    public GetTodoItemsWithPaginationEndpoint(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRoute()
            .MapGet("",                        
                ([AsParameters]GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken) => 
                    HandleAsync(request, cancellationToken))
            .WithDescription("Used to get a list of todos")
            .Produces<PaginatedListResponse<GetToDoItemsResponse>>(StatusCodes.Status200OK);
    }

    public async Task<PaginatedListResponse<GetToDoItemsResponse>> HandleAsync(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetTodoItemsWithPaginationQuery>(request);

        var data = await _mediator.Send(query, cancellationToken);

        var mappedData = _mapper.Map<PaginatedListResponse<GetToDoItemsResponse>>(data);

        return mappedData;
    }
}
