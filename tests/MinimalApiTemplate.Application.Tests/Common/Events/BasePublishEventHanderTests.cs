using MediatR;
using MinimalApiTemplate.Application.Common.Events;
using MinimalApiTemplate.Messages.Common;

namespace MinimalApiTemplate.Application.Tests.Common.Events;

public class BasePublishEventHanderTests : BaseTestFixture<FakePublishEventHander>
{
    private readonly FakePublishEventHander _hander;

    public BasePublishEventHanderTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _hander = new(_publishMessageServiceMock, _currentUserServiceMock, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_Should_PublishMessage()
    {
        var testEvent = new FakeTestEvent(1);

        await _hander.Handle(testEvent, CancellationToken.None);

        await _publishMessageServiceMock.Received(1)
            .Publish<FakeTestMessage>(Arg.Any<FakeTestMessage>(), Arg.Any<CancellationToken>());
    }
}


public class FakePublishEventHander
    : BasePublishExternalEventHander<FakeTestEvent, FakeTestMessage>
{
    public FakePublishEventHander(
        IPublishMessageService publishMessageService, 
        ICurrentUserService currentUserService,
        IMapper mapper, 
        ILogger<FakePublishEventHander> logger) 
        : base(publishMessageService, currentUserService, mapper, logger)
    {
    }

    protected override FakeTestMessage MapMessage(FakeTestEvent notification)
    {
        return new FakeTestMessage
        {
            Id = notification.Id,
            CorrelationId = Guid.NewGuid().ToString(),
        };
    }
}


public record FakeTestEvent(long Id) : INotification;

public class FakeTestMessage : BaseMessage
{
    public long Id { get; set; }
}