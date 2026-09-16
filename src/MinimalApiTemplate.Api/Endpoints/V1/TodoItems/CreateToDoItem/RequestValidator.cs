namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
