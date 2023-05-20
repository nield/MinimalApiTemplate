using MinimalApiTemplate.Application.Common.Models;

namespace MinimalApiTemplate.Application.Tests.Common.Models;

public class PaginatedListTests
{
    private readonly List<SampleTestModel> _items;

    public PaginatedListTests()
    {
        _items = new List<SampleTestModel> 
        { 
            new SampleTestModel { Id = 1 }
        };   
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Given_InvalidPageNumber_When_ClassIsInitialized_Then_ExceptionIsThrown(int pageNumber)
    {
        var sut = Assert.Throws<ArgumentException>(() => new PaginatedList<SampleTestModel>(
                                                            _items, 1, pageNumber, 1));

        sut.ParamName.Should().Be("pageNumber");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Given_InvalidPageSize_When_ClassIsInitialized_Then_ExceptionIsThrown(int pageSize)
    {
        var sut = Assert.Throws<ArgumentException>(() => new PaginatedList<SampleTestModel>(
                                                            _items, 1, 1, pageSize));

        sut.ParamName.Should().Be("pageSize");
    }

    [Fact]   
    public void Given_NegativeCount_When_ClassIsInitialized_Then_ExceptionIsThrown()
    {
        var sut = Assert.Throws<ArgumentException>(() => new PaginatedList<SampleTestModel>(
                                                            _items, -1, 1, 1));

        sut.ParamName.Should().Be("count");
    }

    [Fact]
    public void Given_ZeroCount_When_ClassIsInitialized_Then_ExceptionIsThrown()
    {
        var sut = new PaginatedList<SampleTestModel>(_items, 0, 1, 1);

        sut.TotalCount.Should().Be(0);  
    }

    [Fact]
    public void When_PageNumberIsLessThanTotalPages_Then_HasNextPage_Should_BeTrue()
    {
        _items.Add(new SampleTestModel { Id = 2 });

        var sut = new PaginatedList<SampleTestModel>(_items, 2, 1, 1);

        sut.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void When_PageNumberIsEqualToTotalPages_Then_HasNextPage_Should_BeFalse()
    {
        _items.Add(new SampleTestModel { Id = 2 });

        var sut = new PaginatedList<SampleTestModel>(_items, 2, 2, 1);

        sut.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public void When_PageNumberIsLessThanTotalPagesAndNotFirstPage_Then_HasPreviousPage_Should_BeTrue()
    {
        var sut = new PaginatedList<SampleTestModel>(_items, 10, 2, 1);

        sut.HasPreviousPage.Should().BeTrue();
    }

    public void When_PageNumberIsEqualToTotalPages_Then_HasPreviousPage_Should_BeTrue()
    {
        var sut = new PaginatedList<SampleTestModel>(_items, 2, 2, 1);

        sut.HasPreviousPage.Should().BeTrue();
    }

    public void When_PageNumberIsEqualToFirstPage_Then_HasPreviousPage_Should_BeFalse()
    {
        var sut = new PaginatedList<SampleTestModel>(_items, 2, 1, 1);

        sut.HasPreviousPage.Should().BeFalse();
    }
}

public class SampleTestModel
{
    public int Id { get; set; }
}
