namespace MinimalApiTemplate.Api.Models;

public class PaginatedListResponse<T>
{
    public List<T> Items { get; set; } = null!;
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
