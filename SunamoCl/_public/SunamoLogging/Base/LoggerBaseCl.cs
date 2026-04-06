namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
/// Base class for loggers. Other logger classes like DebugLogger are derived from this.
/// </summary>
public abstract class LoggerBaseCl
{
    /// <summary>
    /// Gets or sets the delegate used to write formatted lines to output.
    /// </summary>
    protected Action<string, string[]> WriteLineDelegate { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the logger is active and should write output.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the LoggerBaseCl class with no write delegate
    /// </summary>
    protected LoggerBaseCl()
    {
    }

    /// <summary>
    /// Initializes a new instance of the LoggerBaseCl class with the specified write delegate
    /// </summary>
    /// <param name="writeLineDelegate">Delegate used to write formatted output lines</param>
    public LoggerBaseCl(Action<string, string[]> writeLineDelegate)
    {
        WriteLineDelegate = writeLineDelegate;
    }

    /// <summary>
    /// Writes a formatted line using the write delegate. Kept for backward compatibility.
    /// </summary>
    /// <param name="formatString">Format string for the message</param>
    /// <param name="args">Arguments for the format string</param>
    public void WriteLineFormat(string formatString, params string[] args)
    {
        WriteLine(formatString, args);
    }

    /// <summary>
    /// Writes the count of items in a collection with the collection name
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="list">Collection to get the count from</param>
    public void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    /// <summary>
    /// Writes a named list header followed by all elements
    /// </summary>
    /// <param name="collectionName">Name of the collection to display as header</param>
    /// <param name="list">List of string elements to write</param>
    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    /// <summary>
    /// Writes all items in a list joined by a separator on a single row
    /// </summary>
    /// <param name="items">Items to join and write</param>
    /// <param name="separator">Separator between items</param>
    public void WriteListOneRow(List<string> items, string separator)
    {
        WriteLineDelegate.Invoke(string.Join(separator, items), []);
    }

    /// <summary>
    /// Writes arguments joined by semicolons
    /// </summary>
    /// <param name="args">Arguments to write</param>
    public void WriteArgs(params string[] args)
    {
        WriteLineDelegate.Invoke(string.Join(";", args), []);
    }

    /// <summary>
    /// Checks whether the given text with arguments can be formatted without exceptions
    /// </summary>
    /// <param name="text">Format string to test</param>
    /// <param name="args">Arguments for the format string</param>
    /// <returns>True if the format is valid, false otherwise</returns>
    public bool IsInRightFormat(string text, params string[] args)
    {
        try
        {
            WriteLineDelegate.Invoke(text, args);
        }
        catch (Exception ex)
        {
            ThrowEx.CustomWithStackTrace(ex);
            return false;
        }

        return true;
    }


    /// <summary>
    /// Writes a formatted line if the logger is active
    /// </summary>
    /// <param name="text">Format string for the message</param>
    /// <param name="args">Arguments for the format string</param>
    public void WriteLine(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(text, args);
    }

    /// <summary>
    /// Writes a formatted line with null-safe text conversion if the logger is active
    /// </summary>
    /// <param name="text">Text to write, converted to "(null)" if null</param>
    /// <param name="args">Arguments for the format string</param>
    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
    }

    /// <summary>
    /// Writes a single line to the output if the text is not null.
    /// </summary>
    /// <param name="text">Text to write.</param>
    public void WriteLine(string text)
    {
        if (text != null) WriteLine(text, Array.Empty<string>());
    }

    /// <summary>
    /// Writes a name-value pair to the output, auto-appending ": " between them.
    /// </summary>
    /// <param name="objectName">Name of the object or property.</param>
    /// <param name="objectValue">Value of the object or property.</param>
    public void WriteLine(string objectName, object objectValue)
    {
        if (objectValue == null) objectValue = "(null)";


        var append = string.Empty;
        if (!string.IsNullOrEmpty(objectName)) append = objectName + ": ";

        WriteLine(append + objectValue);
    }

    /// <summary>
    /// Writes a list with optional numbering and a header
    /// </summary>
    /// <param name="header">Header text for the list</param>
    /// <param name="list">List of string items to write</param>
    /// <param name="isNumbered">Whether to prefix each item with its number</param>
    public void WriteNumberedList(string header, List<string> list, bool isNumbered)
    {
        WriteLineDelegate.Invoke(header + ":", []);
        for (var i = 0; i < list.Count; i++)
            if (isNumbered)
                WriteLine((i + 1).ToString(), list[i]);
            else
                WriteLine(list[i]);
    }

    /// <summary>
    /// Writes each item in the list as a separate line
    /// </summary>
    /// <param name="list">List of string items to write</param>
    public void WriteList(List<string> list)
    {
        list.ForEach(text => WriteLine(text));
    }
}