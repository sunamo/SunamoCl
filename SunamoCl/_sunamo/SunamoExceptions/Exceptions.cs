namespace SunamoCl._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    #region Other

    internal static string CallingMethod(int value = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(value)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(
bool fillAlsoFirstTwo = true)
    {
        StackTrace st = new();
        var value = st.ToString();
        var lines = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var item = lines[i];
            if (fillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static string TextOfExceptions(Exception ex, bool alsoInner = true)
    {
        if (ex == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(ex.Message);
        if (alsoInner)
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                stringBuilder.AppendLine(ex.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var afterAtKeyword = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = afterAtKeyword.Split("(")[0];
        var pathParts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathParts[^1];
        pathParts.RemoveAt(pathParts.Count - 1);
        type = string.Join(".", pathParts);
    }

    #endregion Other

    #region IsNullOrWhitespace

    private static readonly StringBuilder additionalInfo = new();
    private static readonly StringBuilder additionalInfoInner = new();



    #endregion IsNullOrWhitespace

    #region OnlyReturnString

    internal static string? AnyElementIsNullOrEmpty(string before, string nameOfCollection, IEnumerable<int> nulled)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + string.Join(",", nulled) +
        " with null value";
    }

    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    internal static string? DivideByZero(string before)
    {
        return CheckBefore(before) + " is dividing by zero.";
    }

    internal static string? NotEvenNumberOfElements(string before, string nameOfCollection)
    {
        return CheckBefore(before) + nameOfCollection + " have odd elements count";
    }

    #endregion OnlyReturnString

    internal static string? IsNull(string before, string variableName, object? variable)
    {
        return variable == null ? CheckBefore(before) + variableName + " " + "is null" + "." : null;
    }

    internal static string? KeyAlreadyExists<T, U>(string before, Dictionary<T, U> dictionary, T key, string dictionaryName) where T : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            return before + $"Key {key} already exists in {dictionaryName}";
        }
        return null;
    }

    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var fr = string.Empty;
        if (notImplementedName != null)
        {
            fr = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                fr += ((Type)notImplementedName).FullName;
            else
                fr += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + fr + " . internal program error. Please contact developer" +
        ".";
    }
}