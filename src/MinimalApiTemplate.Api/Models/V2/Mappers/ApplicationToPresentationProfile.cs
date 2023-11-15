using MinimalApiTemplate.Api.Models.V2.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Models.V2.Mappers;

public class ApplicationToPresentationProfile : Profile
{
    public ApplicationToPresentationProfile()
    {
        CreateMap<GetToDoItemDto, GetToDoItemResponse>();
    }
}
