using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetToDoItemsMapper : Profile
{
    public GetToDoItemsMapper()
    {
        CreateMap<GetTodoItemsWithPaginationRequest, GetTodoItemsWithPaginationQuery>();

        CreateMap<GetTodoItemsDto, GetToDoItemsResponse>();
    }
}
