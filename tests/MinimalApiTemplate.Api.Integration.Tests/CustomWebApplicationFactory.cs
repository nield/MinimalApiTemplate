﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiTemplate.Api.Integration.Tests.Containers;
using MinimalApiTemplate.Api.Integration.Tests.Mocks;
using MinimalApiTemplate.Application.Common;
using MinimalApiTemplate.Application.Common.Interfaces;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Integration.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    public string DefaultUserId { get; set; } = "1";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable(
            "DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE",
            "false"
        );

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__SqlDatabase",
            DatabaseContainer.Instance.GetConnectionString()
        );

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__Redis",
            CacheContainer.Instance.GetConnectionString()
        );

        builder.UseEnvironment(Constants.Environments.Test);

        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<ICurrentUserService, MockCurrentUserService>();

            services.Configure<TestAuthHandlerOptions>(
                options => options.DefaultUserId = DefaultUserId
            );

            services
                .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options => { }
                );

            services.AddAuthorizationBuilder()
                .AddPolicy(Policies.AdminUser, policy =>
                    policy.RequireAuthenticatedUser())
                .AddPolicy(Policies.StandardUser, policy =>
                    policy.RequireAuthenticatedUser());
        });

        base.ConfigureWebHost(builder);
    }
}
