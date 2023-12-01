using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Infrastructure.Metrics;

public abstract class BaseMetric
{
    protected readonly IMeterFactory _meterFactory;

    protected readonly Meter _meter;

    protected BaseMetric(IMeterFactory meterFactory)
    {
        _meterFactory = meterFactory;

        _meter = meterFactory.Create(MetricMeters.GeneralMeter);
    }
}
