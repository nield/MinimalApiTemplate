using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Infrastructure.Messaging.Consumers;

public class ToDoItemCreatedConsumer : BaseConsumer<ToDoItemCreated, ToDoItemCreatedConsumer>
{
    public ToDoItemCreatedConsumer(ILogger<ToDoItemCreatedConsumer> logger)
        : base(logger)
    {

    }

    protected override Task ProcessMessage(ConsumeContext<ToDoItemCreated> context)
    {
        _logger.LogInformation("Consumed message: {Message}", context.Message);

        return Task.CompletedTask;
    }
}
