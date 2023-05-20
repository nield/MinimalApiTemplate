namespace MinimalApiTemplate.Api.Tests.Models;

public class MappingTests : BaseTestFixture
{
    public MappingTests(MappingFixture mappingFixture)
        : base(mappingFixture)
    {

    }

    [Fact]
    public void When_AutoMapperConfigurationIsInValid_ExcpetionsAreThrown()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
