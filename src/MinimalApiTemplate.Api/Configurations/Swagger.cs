using Microsoft.Extensions.Options;
using MinimalApiTemplate.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalApiTemplate.Api.Configurations;
public static class Swagger
{
    public static void ConfigureSwagger(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
    }

    public static void UseApiDocumentation(this WebApplication app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            // build a swagger endpoint for each discovered API version
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
    }
}