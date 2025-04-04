using MinimalApiTemplate.Api.Common.Models;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapGet("",
                ([Validate][AsParameters] GetTodoItemsWithPaginationRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
                    HandleAsync(request, sender, cancellationToken))
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a list of todos")
            .WithOpenApi(ops =>
            {
                ops.Parameters[0].Description = "The current page number. The first page is 1";
                ops.Parameters[1].Description = "The number of records on a page";
                ops.Parameters[2].Description = "The tags to filter on. Not required";

                return ops;
            })
            .CacheOutput(builder => builder.SetVaryByQuery(nameof(GetTodoItemsWithPaginationRequest.PageNumber),
                                                            nameof(GetTodoItemsWithPaginationRequest.PageSize),
                                                            nameof(GetTodoItemsWithPaginationRequest.Tags))
                                            .Expire(TimeSpan.FromMinutes(5))
                                            .Tag(OutputCacheTags.ToDoList));
    }

    public static async Task<Ok<PaginatedListResponse<GetToDoItemsResponse>>> HandleAsync(
        GetTodoItemsWithPaginationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {

        var query = request.MapToQuery();

        var data = await sender.Send(query, cancellationToken);

        var mappedData = data.MapToPaginatedList<GetTodoItemsDto, GetToDoItemsResponse>();

        return TypedResults.Ok(mappedData);
    }
}
