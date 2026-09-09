#pragma warning disable IDE0130 // Namespace does not match folder structure
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class EnvironmentExtensions
{
    public static bool IsTest(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName.Equals(Constants.Environments.Test);
    }
}
