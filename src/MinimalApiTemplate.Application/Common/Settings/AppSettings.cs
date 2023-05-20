namespace MinimalApiTemplate.Application.Common.Settings;

public class AppSettings
{
    public bool ApplyMigrations { get; set; }
    public Logs Logs { get; set; } = null!;
}

public class Logs
{
    public Performance Performance { get; set; } = null!;
}

public class Performance
{
    public bool LogSlowRunningHandlers { get; set; }
    public int SlowRunningHandlerThreshold { get; set; }
} 
