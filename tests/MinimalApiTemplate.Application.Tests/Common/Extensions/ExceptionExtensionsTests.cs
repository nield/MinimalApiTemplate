using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiTemplate.Application.Tests.Common.Extensions;

public class ExceptionExtensionsTests
{
    [Fact]
    public void When_ExceptionHasMultipleInnerExceptions_Then_ReturnStringWithAllErrors()
    {
        var exception = new Exception("first", new Exception("second"));

        var sut = exception.GetFullErrorMessage();

        sut.Should().Be("first,second");
    }
}
