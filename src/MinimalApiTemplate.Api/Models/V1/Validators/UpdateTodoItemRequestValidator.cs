using FluentValidation;
using MinimalApiTemplate.Api.Models.V1.Requests;

namespace MinimalApiTemplate.Api.Models.V1.Validators;

public class UpdateTodoItemRequestValidator : AbstractValidator<UpdateTodoItemRequest>
{
    public UpdateTodoItemRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
