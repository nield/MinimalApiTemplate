using MinimalApiTemplate.Api.Common.Extensions;
using MinimalApiTemplate.Api.Common.Models;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", HandleAsync)
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a list of todos")
            .WithTags(OpenApi.Tags.ToDos)
            .CacheOutput(builder => builder.SetVaryByQuery(nameof(GetTodoItemsWithPaginationRequest.PageNumber),
                                                            nameof(GetTodoItemsWithPaginationRequest.PageSize),
                                                            nameof(GetTodoItemsWithPaginationRequest.Tags))
                                            .Expire(TimeSpan.FromMinutes(5))
                                            .Tag(OutputCacheTags.ToDoList));
    }

    public static async Task<Ok<PaginatedListResponse<GetToDoItemsResponse>>> HandleAsync(
        [Validate][AsParameters] GetTodoItemsWithPaginationRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {

        var query = request.MapToQuery();

        var data = await sender.Send(query, cancellationToken);

        var mappedData = data.MapToPaginatedList<GetTodoItemsDto, GetToDoItemsResponse>();

        return TypedResults.Ok(mappedData);
    }
}
