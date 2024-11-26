namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationRequestValidator : AbstractValidator<GetTodoItemsWithPaginationRequest>
{
    public GetTodoItemsWithPaginationRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");
    }
}

