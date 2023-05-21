namespace MinimalApiTemplate.Api.Models.V1.Responses;

public class GetToDoItemsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsDone { get; set; }
}
