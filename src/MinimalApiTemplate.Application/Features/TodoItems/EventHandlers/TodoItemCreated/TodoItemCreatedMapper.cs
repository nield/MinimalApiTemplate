using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

public class TodoItemCreatedMapper : Profile
{
    public TodoItemCreatedMapper()
    {
        CreateMap<TodoItemCreatedEvent, ToDoItemCreated>()
            .ForMember(dest => dest.CorrelationId, opt => opt.Ignore());
    }
}
