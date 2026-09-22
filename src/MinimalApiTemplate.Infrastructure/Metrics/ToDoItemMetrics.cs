namespace MinimalApiTemplate.Infrastructure.Metrics;

public class ToDoItemMetrics : BaseMetric, IToDoItemMetrics
{
    private readonly Counter<int> _toDoCreatedCounter;
    private readonly Counter<int> _toDoDeletedCounter;

    public ToDoItemMetrics(IMeterFactory meterFactory)
        : base(meterFactory, Constants.MetricMeters.GeneralMeter)
    {
        _toDoCreatedCounter = _meter.CreateCounter<int>("minimalapitemplate.todo.created");
        _toDoDeletedCounter = _meter.CreateCounter<int>("minimalapitemplate.todo.deleted");
    }

    public void ToDoItemsCreated(string status) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));

    public void ToDoItemsCreatedEventProcessed(string status) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));

    public void ToDoItemsDeleted(string status) =>
        _toDoDeletedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));

    public void ToDoItemsDeletedEventProcessed(string status) =>
        _toDoDeletedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));
    
}
