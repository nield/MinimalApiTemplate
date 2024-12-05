namespace MinimalApiTemplate.Application.Common.Interfaces.Metrics;

public interface IToDoItemMetrics : IMetric
{
    void ToDoItemsCreated(string status);

    void ToDoItemsCreatedEventProcessed(string status);
}
