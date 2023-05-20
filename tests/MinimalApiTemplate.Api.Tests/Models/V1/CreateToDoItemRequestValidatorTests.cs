using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Validators;

namespace MinimalApiTemplate.Api.Tests.Models.V1;

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
