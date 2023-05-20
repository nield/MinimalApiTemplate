using Ardalis.GuardClauses;

namespace MinimalApiTemplate.Application.Common.Models;

public class PaginatedList<T>
{
    public IList<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }

    public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = Guard.Against.Negative(count);
        PageNumber = Guard.Against.NegativeOrZero(pageNumber);
        PageSize = Guard.Against.NegativeOrZero(pageSize);
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}
