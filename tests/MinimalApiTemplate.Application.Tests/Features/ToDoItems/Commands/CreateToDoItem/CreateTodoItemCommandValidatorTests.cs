using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Application.Tests.Features.ToDoItems.Commands.CreateToDoItem;

public class CreateTodoItemCommandValidatorTests
{
    private readonly CreateTodoItemCommandValidator _validator = new();

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var query = new CreateTodoItemCommand { Title = "" };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
