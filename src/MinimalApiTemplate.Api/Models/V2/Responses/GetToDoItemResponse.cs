namespace MinimalApiTemplate.Api.Models.V2.Responses;

public class GetToDoItemResponse
{
    public long Id { get; set; }

    public required string Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public bool IsDone { get; set; }
}
