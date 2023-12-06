namespace MinimalApiTemplate.Api.Models.V1.Responses;

public class GetToDoItemsResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsDone { get; set; }
}
