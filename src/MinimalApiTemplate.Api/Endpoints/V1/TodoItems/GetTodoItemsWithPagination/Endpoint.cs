using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class Endpoint : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute(ApiRoutes.Todos, HandleAsync)
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a list of todos")
            .WithTags(ApiTags.ToDos)
            .CacheOutput(builder => builder.SetVaryByQuery(nameof(Request.PageNumber),
                                                            nameof(Request.PageSize),
                                                            nameof(Request.Tags))
                                            .Expire(TimeSpan.FromMinutes(5))
                                            .Tag(OutputCacheTags.ToDoList));
    }

    public static async Task<Ok<PaginatedListResponse<Response>>> HandleAsync(
        [Validate][AsParameters] Request request,
        ISender sender,
        CancellationToken cancellationToken)
    {

        var query = request.MapToQuery();

        var data = await sender.Send(query, cancellationToken);

        var mappedData = data.MapToPaginatedList<GetTodoItemsDto, Response>(
            Mapper.MapToResponse);

        return TypedResults.Ok(mappedData);
    }
}
