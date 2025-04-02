using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MinimalApiTemplate.Api.Configurations;

public static class Auth
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration config)
    {       
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = config["AuthorityOptions:Authority"];
                options.Audience = config["AuthorityOptions:Audience"];
                options.MetadataAddress = config["AuthorityOptions:MetaDataUrl"]!;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = config["AuthorityOptions:Issuer"]
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        AddKeycloakRolesAsClaims(context);

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.StandardUser, policy => 
                policy.RequireRole(Roles.StandardUserRole));

            options.AddPolicy(Policies.AdminUser, policy =>
                policy.RequireRole(Roles.AdminUserRole));
        });
    }

    private static void AddKeycloakRolesAsClaims(TokenValidatedContext context)
    {
        var user = context.Principal;
        var identity = user?.Identity as ClaimsIdentity;

        var realmRolesClaim = user?.FindFirst("realm_access");

        if (realmRolesClaim is not null && identity is not null)
        {
            var roles = System.Text.Json.JsonDocument.Parse(realmRolesClaim.Value)
                .RootElement.GetProperty("roles")
                .EnumerateArray()
                .Select(r => r.GetString() ?? "");

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role)); // Map to default role claim
            }
        }
    }
}
