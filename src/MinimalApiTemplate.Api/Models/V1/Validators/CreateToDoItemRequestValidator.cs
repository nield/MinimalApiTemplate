using FluentValidation;
using MinimalApiTemplate.Api.Models.V1.Requests;

namespace MinimalApiTemplate.Api.Models.V1.Validators;
public class CreateToDoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
{
    public CreateToDoItemRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
