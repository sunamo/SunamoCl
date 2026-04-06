namespace SunamoCl._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods that check conditions and throw exceptions with detailed context.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">Primary error message.</param>
    /// <param name="isReallyThrow">Whether to actually throw the exception.</param>
    /// <param name="secondMessage">Additional message to append.</param>
    internal static bool Custom(string message, bool isReallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionMessage, isReallyThrow);
    }

    /// <summary>
    /// Throws a custom exception with the stack trace from the specified exception.
    /// </summary>
    /// <param name="exception">Source exception to extract message from.</param>
    internal static bool CustomWithStackTrace(Exception exception)
    { return Custom(Exceptions.TextOfExceptions(exception)); }

    /// <summary>
    /// Throws a divide-by-zero exception.
    /// </summary>
    internal static bool DivideByZero()
    { return ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode())); }

    /// <summary>
    /// Throws an exception if the variable is null.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="variable">The variable value to check.</param>
    internal static bool IsNull(string variableName, object? variable = null)
    { return ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(), variableName, variable)); }

    /// <summary>
    /// Throws an exception for a not-implemented case.
    /// </summary>
    /// <param name="notImplementedName">The unhandled case value.</param>
    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }

    /// <summary>
    /// Gets the full name of the currently executed code (type and method).
    /// </summary>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Breaks into debugger and throws if the exception message is not null.
    /// </summary>
    /// <param name="exceptionMessage">Exception message to evaluate.</param>
    /// <param name="isReallyThrow">Whether to actually throw.</param>
    internal static bool ThrowIsNotNull(string? exceptionMessage, bool isReallyThrow = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (isReallyThrow)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the full name of the executed code from type and method name.
    /// </summary>
    /// <param name="type">Type object, MethodBase, or string representing the type.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="isFromThrowEx">Whether the call originates from ThrowEx (adjusts stack depth).</param>
    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type resolvedType)
        {
            typeFullName = resolvedType.FullName ?? "Type cannot be get via type is Type";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type actualType = type.GetType();
            typeFullName = actualType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws if the specified key already exists in the dictionary.
    /// </summary>
    /// <typeparam name="T">Key type.</typeparam>
    /// <typeparam name="U">Value type.</typeparam>
    /// <param name="dictionary">Dictionary to check.</param>
    /// <param name="key">Key to check for.</param>
    /// <param name="dictionaryName">Name of the dictionary for the error message.</param>
    internal static bool KeyAlreadyExists<T, U>(Dictionary<T, U> dictionary, T key, string dictionaryName) where T : notnull
    {
        return ThrowIsNotNull(Exceptions.KeyAlreadyExists(FullNameOfExecutedCode(), dictionary, key, dictionaryName));
    }

    /// <summary>
    /// Evaluates a function that returns an exception message and throws if not null.
    /// </summary>
    /// <typeparam name="TArg">Type of the argument to pass to the exception function.</typeparam>
    /// <param name="exceptionFunc">Function that returns an exception message or null.</param>
    /// <param name="argument">Argument to pass to the function.</param>
    internal static bool ThrowIsNotNull<TArg>(Func<string, TArg, string?> exceptionFunc, TArg argument)
    {
        string? exceptionMessage = exceptionFunc(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionMessage);
    }
}