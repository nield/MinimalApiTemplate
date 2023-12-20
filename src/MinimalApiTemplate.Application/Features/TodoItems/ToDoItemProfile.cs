using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<CreateTodoItemCommand, TodoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.IsDone, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<UpdateTodoItemCommand, TodoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.Reminder, opt => opt.Ignore())
            .ForMember(dest => dest.IsDone, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<TodoItem, GetTodoItemsDto>();

        CreateMap<TodoItem, GetToDoItemDto>();

        CreateMap<TodoItem, TodoItemCreatedEvent>();

        CreateMap<TodoItemCreatedEvent, ToDoItemCreated>();
    }
}
