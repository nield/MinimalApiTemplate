namespace MinimalApiTemplate.Application.Common.Settings;

public class AppSettings
{
    public bool ApplyMigrations { get; set; }
    public required Logs Logs { get; set; }
}

public class Logs
{
    public required Performance Performance { get; set; }
}

public class Performance
{
    public bool LogSlowRunningHandlers { get; set; }
    public int SlowRunningHandlerThreshold { get; set; }
}
