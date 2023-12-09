using OpenTelemetry.Metrics;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Configurations;

public static class Metrics
{
    public static void ConfigureMetrics(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder =>            
                builder
                    .AddPrometheusExporter()
                    .AddMeter(MetricMeters.AspNetCoreHosting,
                                MetricMeters.AspNetCoreKestrel,
                                MetricMeters.GeneralMeter)
                    .AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries = [0,
                                0.005,
                                0.01,
                                0.025,
                                0.05,
                                0.075,
                                0.1,
                                0.25,
                                0.5,
                                0.75,
                                1,
                                2.5,
                                5,
                                7.5,
                                10]
                        }));            
    }
}
