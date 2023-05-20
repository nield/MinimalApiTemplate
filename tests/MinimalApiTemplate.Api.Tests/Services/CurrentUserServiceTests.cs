using System.Security.Claims;
using MinimalApiTemplate.Api.Services;
using Microsoft.AspNetCore.Http;

namespace MinimalApiTemplate.Api.Tests.Services;

public class CurrentUserServiceTests
{
    private readonly CurrentUserService _currentUserService;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();

    public CurrentUserServiceTests()
    {
        _currentUserService = new(_httpContextAccessorMock.Object);
    }

    [Fact]
    public void Given_UserIdClaimExists_When_FetchingUserId_Then_ReturnsUserIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserProfileId", "1")
            }))
        };

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserId.Should().Be("1");
    }

    [Fact]
    public void Given_UserIdClaimDoesNotExists_When_FetchingUserId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserId.Should().BeNull();
    }

    [Fact]
    public void Given_UserProfileIdClaimExists_When_FetchingUserId_Then_ReturnsUserProfileIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserProfileId", "1")
            }))
        };

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserProfileId.Should().Be("1");
    }

    [Fact]
    public void Given_UserProfileIdClaimDoesNotExists_When_FetchingUserProfileId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserProfileId.Should().BeNull();
    }

    [Fact]
    public void Given_CorrelationIdClaimExists_When_FetchingCorrelationId_Then_ReturnsCorrelationIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("CorrelationId", "1")
            }))
        };

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.CorrelationId.Should().Be("1");
    }

    [Fact]
    public void Given_CorrelationIdClaimDoesNotExists_When_FetchingCorrelationId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.CorrelationId.Should().BeNull();
    }

    [Fact]
    public void Given_AuthorizationClaimExists_When_FetchingToken_Then_ReturnsAuthorizationFromClaim()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Add("Authorization", "token");

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.Token.Should().Be("token");
    }

    [Fact]
    public void GivenAuthorizationClaimDoesNotExists_When_FetchingToken_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.Token.Should().BeNull();
    }
}