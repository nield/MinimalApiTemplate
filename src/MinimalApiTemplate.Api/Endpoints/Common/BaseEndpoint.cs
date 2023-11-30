namespace MinimalApiTemplate.Api.Endpoints.Common;

public abstract class BaseEndpoint
{
    protected readonly ISender _mediator;
    protected readonly IMapper _mapper;

    protected BaseEndpoint(ISender sender, IMapper mapper)
    {
        _mediator = sender;
        _mapper = mapper;
    }
}
