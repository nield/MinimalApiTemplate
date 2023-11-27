using Asp.Versioning.Builder;
using Asp.Versioning;

namespace MinimalApiTemplate.Api.Endpoints;

public static class VersionsSets
{
    private static readonly Dictionary<ApiVersion, ApiVersionSet> _versionSets = new();

    public static ApiVersionSet GetVersionSet(int majorVersion = 1, int minorVersion = 0)
    {
        var key = new ApiVersion(majorVersion, minorVersion);

        if (!_versionSets.TryGetValue(key, out ApiVersionSet? value))
        {
            value = CreateVersionSet(majorVersion, minorVersion);

            _versionSets[key] = value;
        }

        return value!;
    }

    private static ApiVersionSet CreateVersionSet(int majorVersion, int minorVersion)
    {
        return new ApiVersionSetBuilder(null)           
            .ReportApiVersions()            
            .HasApiVersion(new ApiVersion(majorVersion, minorVersion))
            .Build();           
    }
}
