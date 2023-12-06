using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace MinimalApiTemplate.Api.Configurations;

public static class Compression
{
    public static void ConfigureCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options
            => options.Level = CompressionLevel.SmallestSize);

        services.Configure<GzipCompressionProviderOptions>(options
            => options.Level = CompressionLevel.SmallestSize);
    }

    public static void UseCompression(this WebApplication app)
    {
        app.UseResponseCompression();
    }
}
