namespace MinimalApiTemplate.Infrastructure.Metrics;

public class ToDoItemMetrics : BaseMetric, IToDoItemMetrics
{
    private readonly Counter<int> _toDoCreatedCounter;
    private readonly Counter<int> _toDoCreatedEventProcessedCounter;

    public ToDoItemMetrics(IMeterFactory meterFactory)
        : base(meterFactory, Constants.MetricMeters.GeneralMeter)
    {
        _toDoCreatedCounter = _meter.CreateCounter<int>("minimalapitemplate.todo.created");
        _toDoCreatedEventProcessedCounter = _meter.CreateCounter<int>("minimalapitemplate.todo.created.event.processed");
    }

    public void ToDoItemsCreated(string title) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>($"{nameof(title)}", title));

    public void ToDoItemsCreatedEventProcessed(string title)
    {
        _toDoCreatedEventProcessedCounter.Add(1, new KeyValuePair<string, object?>($"{nameof(title)}", title));
    }
}
