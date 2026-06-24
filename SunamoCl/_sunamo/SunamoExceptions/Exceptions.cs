namespace SunamoCl._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    internal static string CallingMethod(int stackDepth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(stackDepth)?.GetMethod();
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
bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string type = string.Empty;
        string methodName = string.Empty;
        for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
        {
            var line = lines[lineIndex];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static string TextOfExceptions(Exception exception, bool isIncludingInner = true)
    {
        if (exception == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(exception.Message);
        if (isIncludingInner)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                stringBuilder.AppendLine(exception.Message);
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

    internal static string? AnyElementIsNullOrEmpty(string before, string nameOfCollection, IEnumerable<int> nullIndexes)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + string.Join(",", nullIndexes) +
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
        var formattedName = string.Empty;
        if (notImplementedName != null)
        {
            formattedName = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                formattedName += ((Type)notImplementedName).FullName;
            else
                formattedName += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + formattedName + " . internal program error. Please contact developer" +
        ".";
    }
}
