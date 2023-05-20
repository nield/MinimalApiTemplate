using MinimalApiTemplate.Application.Common.Events;
using MediatR;

namespace MinimalApiTemplate.Application.Tests.Common.Events;

public class BasePublishEventHanderTests : BaseTestFixture<FakePublishEventHander>
{
    private readonly FakePublishEventHander _hander;

    public BasePublishEventHanderTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _hander = new(_publishMessageServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishMessage()
    {
        var testEvent = new FakeTestEvent(1);

        await _hander.Handle(testEvent, CancellationToken.None);

        _publishMessageServiceMock.Verify(x => x.Publish<FakeTestEvent, FakeTestMessage>(testEvent, It.IsAny<CancellationToken>()), Times.Once);
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