namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class CreateToDoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
{
    public CreateToDoItemRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
