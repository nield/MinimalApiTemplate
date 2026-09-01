using MinimalApiTemplate.Api.Common.Extensions;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

public class GetToDoItemEndpoint : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos/{id}", HandleAsync)
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a single todo")
            .WithName("GetToDoItem")
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OpenApi.Tags.ToDos);
    }

    public static async Task<Ok<GetToDoItemResponse>> HandleAsync(
        [FromRoute] long id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await sender.Send(query, cancellationToken);

        var mappedData = data.MapToResponse();

        return TypedResults.Ok(mappedData);
    }
}
