namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

public class GetToDoItemQueryValidator : AbstractValidator<GetToDoItemQuery>
{
    public GetToDoItemQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
