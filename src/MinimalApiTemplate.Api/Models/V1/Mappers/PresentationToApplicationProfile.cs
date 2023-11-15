using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Models.V1.Mappers;

public class PresentationToApplicationProfile : Profile
{
    public PresentationToApplicationProfile()
    {
        CreateMap<CreateTodoItemRequest, CreateTodoItemCommand>();

        CreateMap<UpdateTodoItemRequest, UpdateTodoItemCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<GetTodoItemsWithPaginationRequest, GetTodoItemsWithPaginationQuery>();
    }
}
