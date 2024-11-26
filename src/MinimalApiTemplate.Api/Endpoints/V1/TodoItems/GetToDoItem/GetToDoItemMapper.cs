using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

public class GetToDoItemMapper : Profile
{
    public GetToDoItemMapper()
    {
        CreateMap<GetToDoItemDto, GetToDoItemResponse>();
    }
}
