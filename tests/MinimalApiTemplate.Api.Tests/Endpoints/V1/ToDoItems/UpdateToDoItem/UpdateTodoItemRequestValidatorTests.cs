using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.UpdateToDoItem;

public class UpdateTodoItemRequestValidatorTests
{
    private readonly UpdateTodoItemRequestValidator _validator = new();

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var query = new UpdateTodoItemRequest { Title = "" };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
