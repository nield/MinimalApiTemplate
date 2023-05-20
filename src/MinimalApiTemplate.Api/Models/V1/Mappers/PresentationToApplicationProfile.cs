using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Models.V1.Mappers;

public class PresentationToApplicationProfile : Profile
{
    public PresentationToApplicationProfile()
    {
        CreateMap<GetTodoItemsDto, GetToDoItemsResponse>();

        CreateMap<GetToDoItemDto, GetToDoItemResponse>();
    }
}
