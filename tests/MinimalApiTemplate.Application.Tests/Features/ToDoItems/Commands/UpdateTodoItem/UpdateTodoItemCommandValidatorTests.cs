using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandValidatorTests
{
    private readonly UpdateTodoItemCommandValidator _validator = new();

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var query = new UpdateTodoItemCommand { Title = "" };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
