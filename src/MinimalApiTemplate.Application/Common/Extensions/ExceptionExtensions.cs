#pragma warning disable IDE0130 // Namespace does not match folder structure
// ReSharper disable once CheckNamespace
namespace System;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ExceptionExtensions
{
    public static string GetFullErrorMessage(this Exception exception)
    {
        var errorList = new List<string>();

        var currentException = exception;

        while (currentException != null)
        {
            errorList.Add(currentException.Message);
            currentException = currentException.InnerException;
        }

        return string.Join(",", errorList);
    }
}
