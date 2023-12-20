using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Infrastructure.Messaging.Consumers;

public class ToDoItemCreatedConsumer : IConsumer<ToDoItemCreated>
{
    private readonly ILogger<ToDoItemCreatedConsumer> _logger;

    public ToDoItemCreatedConsumer(ILogger<ToDoItemCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ToDoItemCreated> context)
    {
        _logger.LogInformation("Consumed message: {Message}", context.Message);

        return Task.CompletedTask;
    }
}
