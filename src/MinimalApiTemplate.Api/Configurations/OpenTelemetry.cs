using OpenTelemetry.Metrics;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Configurations;

public static class OpenTelemetry
{
    public static void ConfigureOpenTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder =>            
                builder
                    .AddPrometheusExporter()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddMeter(MetricMeters.GeneralMeter)
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
