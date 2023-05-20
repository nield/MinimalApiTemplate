using Microsoft.Extensions.Hosting;

namespace MinimalApiTemplate.Application.Tests.Common.Extensions;

public class EnvironmentExtensionsTests
{
    private readonly Mock<IHostEnvironment> _environmentMock = new();

    [Fact]
    public void Given_EnvironmentIsTest_When_CheckingIsTest_Then_IsTrue()
    {
        _environmentMock.SetupGet(x => x.EnvironmentName)
            .Returns(Constants.Environments.Test);

        var sut = _environmentMock.Object.IsTest();

        sut.Should().BeTrue();
    }
}
