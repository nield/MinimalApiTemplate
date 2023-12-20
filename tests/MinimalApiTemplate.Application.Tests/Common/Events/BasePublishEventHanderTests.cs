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
        _hander = new(_publishMessageServiceMock, _mapper, _logger);
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
    : BasePublishExternalEventHander<FakeTestEvent, FakeTestMessage, FakePublishEventHander>
{
    public FakePublishEventHander(
        IPublishMessageService publishMessageService, 
        IMapper mapper, 
        ILogger<FakePublishEventHander> logger) 
        : base(publishMessageService, mapper, logger)
    {
    }
}


public record FakeTestEvent(long Id) : INotification;

public class FakeTestMessage : BaseMessage
{
    public int Id { get; set; }
}