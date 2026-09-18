namespace MinimalApiTemplate.Api.Common;

public static class Constants
{
    public static class ApiTags
    {
        public const string ToDos = "ToDos";
    }

    public static class ApiRoutes
    {
        public const string Todos = "/todos";
    }
    
    public static class OutputCacheTags
    {
        public const string ToDoList = "ToDoList";
    }

    public static class Headers
    {
        public const string CorrelationId = "x-correlation-id";
        public const string Authorization = "Authorization";
    } 
}
