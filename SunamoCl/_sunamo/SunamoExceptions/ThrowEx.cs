namespace SunamoCl._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool Custom(string message, bool isReallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionMessage, isReallyThrow);
    }

    internal static bool CustomWithStackTrace(Exception exception)
    { return Custom(Exceptions.TextOfExceptions(exception)); }

    internal static bool DivideByZero()
    { return ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode())); }

    internal static bool IsNull(string variableName, object? variable = null)
    { return ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(), variableName, variable)); }

    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }

    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

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

    internal static bool KeyAlreadyExists<T, U>(Dictionary<T, U> dictionary, T key, string dictionaryName) where T : notnull
    {
        return ThrowIsNotNull(Exceptions.KeyAlreadyExists(FullNameOfExecutedCode(), dictionary, key, dictionaryName));
    }

    internal static bool ThrowIsNotNull<TArg>(Func<string, TArg, string?> exceptionFunc, TArg argument)
    {
        string? exceptionMessage = exceptionFunc(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionMessage);
    }
}
