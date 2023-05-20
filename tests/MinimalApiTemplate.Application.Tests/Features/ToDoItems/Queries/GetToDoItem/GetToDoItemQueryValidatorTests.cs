using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Queries.GetToDoItem;

public class GetToDoItemQueryValidatorTests
{
    private readonly GetToDoItemQueryValidator _validator = new();

    [Fact]
    public void Given_InvalidId_Should_HaveError()
    {
        var query = new GetToDoItemQuery { Id = 0 };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
