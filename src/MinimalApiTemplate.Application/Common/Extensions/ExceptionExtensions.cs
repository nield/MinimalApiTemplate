namespace System;

public static class ExceptionExtensions
{
    public static string GetFullErrorMessage(this Exception exception)
    {
        if (exception == null) return string.Empty;

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
