﻿namespace MinimalApiTemplate.Api.Common;

public static class Constants
{
    public static class OpenApi
    {
        public static class Tags
        {
            public const string ToDos = "ToDos";
        }
    }

    public static class OutputCacheTags
    {
        public const string ToDoList = "ToDoList";
    }

    public static class Headers
    {
        public const string CorrelationId = "x-correlation-id";
        public const string UserProfileId = "UserProfileId";
        public const string Authorization = "Authorization";
    } 
}
