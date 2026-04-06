namespace SunamoCl._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for formatting and building exception messages.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Returns the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="stackDepth">Depth in the stack trace (default is 1 for the direct caller).</param>
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

    /// <summary>
    /// Formats the context prefix for exception messages.
    /// </summary>
    /// <param name="before">Context prefix text.</param>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets the type, method name, and full stack trace of the exception location.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to also fill the type and method name from the first relevant frame.</param>
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

    /// <summary>
    /// Builds a text representation of the exception including inner exceptions.
    /// </summary>
    /// <param name="exception">Exception to format.</param>
    /// <param name="isIncludingInner">Whether to include inner exceptions.</param>
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

    /// <summary>
    /// Extracts the type and method name from a single stack trace line.
    /// </summary>
    /// <param name="stackTraceLine">A single line from the stack trace.</param>
    /// <param name="type">Extracted type name.</param>
    /// <param name="methodName">Extracted method name.</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var afterAtKeyword = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = afterAtKeyword.Split("(")[0];
        var pathParts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathParts[^1];
        pathParts.RemoveAt(pathParts.Count - 1);
        type = string.Join(".", pathParts);
    }

    /// <summary>
    /// Returns a formatted message listing indexes of null or empty elements.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    /// <param name="nameOfCollection">Name of the collection.</param>
    /// <param name="nulled">Indexes of null or empty elements.</param>
    internal static string? AnyElementIsNullOrEmpty(string before, string nameOfCollection, IEnumerable<int> nulled)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + string.Join(",", nulled) +
        " with null value";
    }

    /// <summary>
    /// Returns a custom formatted exception message.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    /// <param name="message">Custom message text.</param>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    /// <summary>
    /// Returns a divide-by-zero error message.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    internal static string? DivideByZero(string before)
    {
        return CheckBefore(before) + " is dividing by zero.";
    }

    /// <summary>
    /// Returns a message indicating the collection has an odd number of elements.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    /// <param name="nameOfCollection">Name of the collection.</param>
    internal static string? NotEvenNumberOfElements(string before, string nameOfCollection)
    {
        return CheckBefore(before) + nameOfCollection + " have odd elements count";
    }

    /// <summary>
    /// Returns a message if the variable is null, otherwise returns null.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    /// <param name="variableName">Name of the variable being checked.</param>
    /// <param name="variable">The variable to check for null.</param>
    internal static string? IsNull(string before, string variableName, object? variable)
    {
        return variable == null ? CheckBefore(before) + variableName + " " + "is null" + "." : null;
    }

    /// <summary>
    /// Returns a message if the key already exists in the dictionary, otherwise returns null.
    /// </summary>
    /// <typeparam name="T">Key type.</typeparam>
    /// <typeparam name="U">Value type.</typeparam>
    /// <param name="before">Context prefix.</param>
    /// <param name="dictionary">Dictionary to check.</param>
    /// <param name="key">Key to look for.</param>
    /// <param name="dictionaryName">Name of the dictionary for the message.</param>
    internal static string? KeyAlreadyExists<T, U>(string before, Dictionary<T, U> dictionary, T key, string dictionaryName) where T : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            return before + $"Key {key} already exists in {dictionaryName}";
        }
        return null;
    }

    /// <summary>
    /// Returns a message indicating a case is not implemented.
    /// </summary>
    /// <param name="before">Context prefix.</param>
    /// <param name="notImplementedName">The unhandled case value or type.</param>
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