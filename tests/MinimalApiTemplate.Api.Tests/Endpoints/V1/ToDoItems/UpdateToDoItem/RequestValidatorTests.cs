using MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

namespace MinimalApiTemplate.Api.Tests.Endpoints.V1.ToDoItems.UpdateToDoItem;

public class RequestValidatorTests
{
    private readonly RequestValidator _validator = new();

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var query = new Request { Title = "" };

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
