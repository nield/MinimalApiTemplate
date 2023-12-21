namespace MinimalApiTemplate.Application.Common.Interfaces.Metrics;

public interface IToDoItemMetrics : IMetric
{
    void ToDoItemsCreated(string title);

    void ToDoItemsCreatedEventProcessed(string title);
}
