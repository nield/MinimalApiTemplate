using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Common.Events;

[Mapper]
public static partial class EventMapper
{
    public static partial TDest MapMessage<TSource, TDest>(TSource source)
        where TSource : INotification
        where TDest : BaseMessage;

    public static TDest MapToMessage<TSource, TDest>(this TSource source)
        where TSource : INotification
        where TDest : BaseMessage
    {
        return MapMessage<TSource, TDest>(source);
    }

    private static partial ToDoItemCreated MapToDoItemCreated(TodoItemCreatedEvent source);
}
