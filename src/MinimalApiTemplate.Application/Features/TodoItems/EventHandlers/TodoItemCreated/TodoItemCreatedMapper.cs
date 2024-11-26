using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

public class TodoItemCreatedMapper : Profile
{
    public TodoItemCreatedMapper()
    {
        CreateMap<TodoItem, TodoItemCreatedEvent>();

        CreateMap<TodoItemCreatedEvent, ToDoItemCreated>()
            .ForMember(dest => dest.CorrelationId, opt => opt.Ignore());
    }
}
