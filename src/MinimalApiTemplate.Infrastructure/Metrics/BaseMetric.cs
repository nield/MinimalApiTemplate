namespace MinimalApiTemplate.Infrastructure.Metrics;

public abstract class BaseMetric
{
    protected readonly IMeterFactory _meterFactory;

    protected readonly Meter _meter;

    protected BaseMetric(IMeterFactory meterFactory, string meterName)
    {
        _meterFactory = meterFactory;

        _meter = meterFactory.Create(meterName);
    }
}
