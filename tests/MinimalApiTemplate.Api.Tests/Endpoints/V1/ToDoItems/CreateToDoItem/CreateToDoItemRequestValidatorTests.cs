using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.CreateToDoItem;

public class CreateToDoItemRequestValidatorTests
{
    private readonly CreateToDoItemRequestValidator _validator = new();

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var query = new CreateTodoItemRequest { Title = "" };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
