namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemMapper : Profile
{
    public UpdateTodoItemMapper()
    {
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
    }
}
