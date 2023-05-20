using MinimalApiTemplate.Api.Integration.Tests.Mocks;
using MinimalApiTemplate.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTemplate.Api.Integration.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    public string DefaultUserId { get; set; } = "1";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        System.Environment.SetEnvironmentVariable(
            "DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE",
            "false"
        );

        System.Environment.SetEnvironmentVariable(
            "ConnectionStrings__DefaultConnection",
            Environment.DatabaseConnectionString
        );

        System.Environment.SetEnvironmentVariable(
            "RedisOptions__ConnectionString",
            Environment.CacheConnectionString
        );

        builder.ConfigureTestServices(services =>
        {
            services.Configure<TestAuthHandlerOptions>(
                options => options.DefaultUserId = DefaultUserId
            );

            services
                .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options => { }
                );

            services.AddScoped<ICurrentUserService, MockCurrentUserService>();
        });

        base.ConfigureWebHost(builder);
    }
}
