using MediatR;
using MinimalApiTemplate.Application.Common.Events;

namespace MinimalApiTemplate.Application.Tests.Common.Events;

public class BasePublishEventHanderTests : BaseTestFixture<FakePublishEventHander>
{
    private readonly FakePublishEventHander _hander;

    public BasePublishEventHanderTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _hander = new(_publishMessageServiceMock);
    }

    [Fact]
    public async Task Handle_Should_PublishMessage()
    {
        var testEvent = new FakeTestEvent(1);

        await _hander.Handle(testEvent, CancellationToken.None);

        await _publishMessageServiceMock.Received(1)
            .Publish<FakeTestEvent, FakeTestMessage>(testEvent, Arg.Any<CancellationToken>());
    }
}


public class FakePublishEventHander : BasePublishEventHander<FakeTestEvent, FakeTestMessage>
{
    public FakePublishEventHander(IPublishMessageService publishMessageService)
        : base(publishMessageService)
    {

    }
}


public record FakeTestEvent(long Id) : INotification;

public record FakeTestMessage(long Id);