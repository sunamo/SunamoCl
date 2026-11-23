namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
/// Represents the logger base cl type.
/// </summary>
public abstract class LoggerBaseCl
{
    /// <summary>
    /// Gets or sets the write line delegate.
    /// </summary>
    protected Action<string, string[]> WriteLineDelegate { get; set; } = null!;

    /// <summary>
    /// Gets or sets the is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Logger base cl.
    /// </summary>
    /// <returns>The logger base cl result.</returns>
    protected LoggerBaseCl()
    {
    }

    /// <summary>
    /// Logger base cl.
    /// </summary>
    /// <returns>The logger base cl result.</returns>
    public LoggerBaseCl(Action<string, string[]> writeLineDelegate)
    {
        WriteLineDelegate = writeLineDelegate;
    }

    /// <summary>
    /// Initializes a new instance of the write line format class.
    /// </summary>
    /// <param name="writeLineDelegate">The write line delegate.</param>
    public void WriteLineFormat(string formatString, params string[] args)
    {
        WriteLine(formatString, args);
    }

    /// <summary>
    /// Write count.
    /// </summary>
    /// <param name="formatString">The format string.</param>
    /// <param name="args">The args.</param>
    public void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    /// <summary>
    /// Write list.
    /// </summary>
    /// <param name="collectionName">The collection name.</param>
    /// <param name="list">The list.</param>
    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    /// <summary>
    /// Write list one row.
    /// </summary>
    /// <param name="collectionName">The collection name.</param>
    /// <param name="list">The list.</param>
    public void WriteListOneRow(List<string> items, string separator)
    {
        WriteLineDelegate.Invoke(string.Join(separator, items), []);
    }

    /// <summary>
    /// Write args.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="separator">The separator.</param>
    public void WriteArgs(params string[] args)
    {
        WriteLineDelegate.Invoke(string.Join(";", args), []);
    }

    /// <summary>
    /// Is in right format.
    /// </summary>
    /// <param name="args">The args.</param>
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
    /// Write line.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    /// <returns>The write line result.</returns>
    public void WriteLine(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(text, args);
    }

    /// <summary>
    /// Write line null.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
    }

    /// <summary>
    /// Write line.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void WriteLine(string text)
    {
        if (text != null) WriteLine(text, Array.Empty<string>());
    }

    /// <summary>
    /// Write line.
    /// </summary>
    /// <param name="text">The text.</param>
    public void WriteLine(string objectName, object objectValue)
    {
        if (objectValue == null) objectValue = "(null)";

        var append = string.Empty;
        if (!string.IsNullOrEmpty(objectName)) append = objectName + ": ";

        WriteLine(append + objectValue);
    }

    /// <summary>
    /// Write numbered list.
    /// </summary>
    /// <param name="!string.IsNullOrEmpty(objectName)">The string is null or empty object name.</param>
    /// <returns>The write numbered list result.</returns>
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
    /// Initializes a new instance of the write list class.
    /// </summary>
    /// <param name="isNumbered">The is numbered.</param>
    public void WriteList(List<string> list)
    {
        list.ForEach(text => WriteLine(text));
    }
}
