using FluentValidation.Results;

namespace MinimalApiTemplate.Application.Tests.Common.Exceptions;

public class DataValidationFailureExceptionTests
{
    [Fact]
    public void When_NoValidationFailuresExists_Then_DictionaryIsEmpty()
    {
        var actual = new DataValidationFailureException().Errors;

        actual.Keys.Should().BeEmpty();
    }

    [Fact]
    public void When_SingleValidationFailureExists_Then_DictionaryContainsSingleItem()
    {
        var failures = new List<ValidationFailure>
            {
                new("Age", "must be over 18"),
            };

        var actual = new DataValidationFailureException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Age");
        actual["Age"].Should().BeEquivalentTo("must be over 18");
    }

    [Fact]
    public void When_MultipleValidationFailureExistsForMultipleProperties_Then_DictionaryContainsEachFailureWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
            {
                new("Age", "must be 18 or older"),
                new("Age", "must be 25 or younger"),
                new("Password", "must contain at least 8 characters"),
                new("Password", "must contain a digit"),
                new("Password", "must contain upper case letter"),
                new("Password", "must contain lower case letter"),
            };

        var actual = new DataValidationFailureException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Password", "Age");

        actual["Age"].Should().BeEquivalentTo(
            "must be 25 or younger",
            "must be 18 or older"
        );

        actual["Password"].Should().BeEquivalentTo(        
            "must contain lower case letter",
            "must contain upper case letter",
            "must contain at least 8 characters",
            "must contain a digit"
        );
    }
}
