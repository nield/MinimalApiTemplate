namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
