﻿namespace MinimalApiTemplate.Api.Models.V1.Responses;

public class GetToDoItemResponse
{
    public long Id { get; set; }

    public required string Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }

    public bool IsDone { get; set; }

    public List<string> Tags { get; set; } = [];
}
