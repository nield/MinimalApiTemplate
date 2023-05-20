using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Validators;

namespace MinimalApiTemplate.Api.Tests.Models.V1;

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
