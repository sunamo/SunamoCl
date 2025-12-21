namespace SunamoCl._sunamo.SunamoExceptions;

internal partial class ThrowEx
{

    internal static bool Custom(string message, bool reallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? str = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(str, reallyThrow);
    }

    internal static bool CustomWithStackTrace(Exception ex)
    { return Custom(Exceptions.TextOfExceptions(ex)); }

    internal static bool DivideByZero()
    { return ThrowIsNotNull(Exceptions.DivideByZero(FullNameOfExecutedCode())); }

    internal static bool IsNull(string variableName, object? variable = null)
    { return ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(), variableName, variable)); }


    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }

    #region Other

    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfExc = Exceptions.PlaceOfException();
        string f = FullNameOfExecutedCode(placeOfExc.Item1, placeOfExc.Item2, true);
        return f;
    }

    internal static bool ThrowIsNotNull(string? exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (reallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type type2)
        {
            typeFullName = type2.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type t = type.GetType();
            typeFullName = t.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    #region For avoid FullNameOfExecutedCode

    internal static bool KeyAlreadyExists<T, U>(Dictionary<T, U> dictionary, T key, string dictionaryName) where T : notnull
    {
        return ThrowIsNotNull(Exceptions.KeyAlreadyExists(FullNameOfExecutedCode(), dictionary, key, dictionaryName));
    }



    internal static bool ThrowIsNotNull<A>(Func<string, A, string?> f, A ex)
    {
        string? exc = f(FullNameOfExecutedCode(), ex);
        return ThrowIsNotNull(exc);
    }


    #endregion For avoid FullNameOfExecutedCode

    #endregion Other
}