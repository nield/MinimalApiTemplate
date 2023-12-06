using Asp.Versioning;
using Asp.Versioning.Builder;

namespace MinimalApiTemplate.Api.Endpoints.Common;

public static class VersionSets
{
    private static readonly Dictionary<ApiVersion, ApiVersionSet> VersionSetStore = [];

    public static ApiVersionSet GetVersionSet(int majorVersion = 1, int minorVersion = 0)
    {
        ApiVersion key = new(majorVersion, minorVersion);

        if (!VersionSetStore.TryGetValue(key, out ApiVersionSet? value))
        {
            value = CreateVersionSet(majorVersion, minorVersion);

            VersionSetStore[key] = value;
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
