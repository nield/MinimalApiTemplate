namespace MinimalApiTemplate.Infrastructure.Metrics;

public class ToDoItemMetrics : BaseMetric, IToDoItemMetrics
{
    private readonly Counter<int> _toDoCreatedCounter;

    public ToDoItemMetrics(IMeterFactory meterFactory)
        : base(meterFactory)
    {
        _toDoCreatedCounter = _meter.CreateCounter<int>("minimaltemplate.todo.created");
    }

    public void ToDoItemsCreated(string title) =>
        _toDoCreatedCounter.Add(1, new KeyValuePair<string, object?>($"{nameof(title)}", title));
}
