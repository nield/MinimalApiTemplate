namespace MinimalApiTemplate.Infrastructure.Metrics;

public class ToDoItemMetrics : BaseMetric, IToDoItemMetrics
{
    private readonly Counter<int> _toDoCreatedCounter;

    public ToDoItemMetrics(IMeterFactory meterFactory)
        : base(meterFactory, Constants.MetricMeters.GeneralMeter)
    {
        _toDoCreatedCounter = _meter.CreateCounter<int>("minimalapitemplate.todo");
    }

    public void ToDoItemsCreated(string status) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));

    public void ToDoItemsCreatedEventProcessed(string status) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>(nameof(status), status));
}
