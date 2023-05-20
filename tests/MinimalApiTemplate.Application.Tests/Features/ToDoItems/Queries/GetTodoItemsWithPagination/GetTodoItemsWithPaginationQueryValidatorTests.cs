using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationQueryValidatorTests
{
    private readonly GetTodoItemsWithPaginationQueryValidator _validator = new();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidPageNumber_Should_HaveError(int pageNumber)
    {
        var query = new GetTodoItemsWithPaginationQuery { PageNumber = pageNumber };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidPageSize_Should_HaveError(int pageSize)
    {
        var query = new GetTodoItemsWithPaginationQuery { PageSize = pageSize };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}
