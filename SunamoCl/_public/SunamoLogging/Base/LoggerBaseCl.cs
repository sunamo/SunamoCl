namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
///     Musí být v sunamo, jsou tu od něj odvozeny další třídy jako např. DebugLogger
/// </summary>
public abstract class LoggerBaseCl //: ILoggerBase
{
    private static Type type = typeof(LoggerBaseCl);

    private StringBuilder stringBuilder = new();

    // TODO: Make logger public class as base and replace all occurences With Instance
    /// <summary>
    /// Delegate used to write formatted lines to output
    /// </summary>
    protected Action<string, string[]> WriteLineDelegate = null!;

    /// <summary>
    /// Gets or sets whether the logger is active and should write output
    /// </summary>
    public bool IsActive = true;

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
    ///     Only for debug purposes
    /// </summary>
    public void ClipboardOrDebug()
    {
        //#if DEBUG
        //        //DebugLogger.DebugWriteLine(TypeOfMessage.Appeal, v, args);
        //#else
        ////stringBuilder.AppendLine(TypeOfMessage.Appeal + ": " + string.Format(v, args));
        ////ClipboardService.SetText(stringBuilder.ToString());
        //#endif
    }

    /// <summary>
    ///     Only due to Old sfw apps
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
        //#if DEBUG
        WriteLineDelegate.Invoke(string.Join(separator, items), []);
        //#endif
    }

    /// <summary>
    /// Writes arguments joined by semicolons
    /// </summary>
    /// <param name="args">Arguments to write</param>
    public void WriteArgs(params string[] args)
    {
        WriteLineDelegate.Invoke( /*SHJoinPairs.JoinPairs(args)*/ string.Join(";", args), []);
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
    ///     for compatibility with CL.WriteLine
    /// </summary>
    /// <param name="what"></param>
    public void WriteLine(string what)
    {
        if (what != null) WriteLine(what, Array.Empty<string>());
    }

    /// <summary>
    ///     Will auto append ": "
    /// </summary>
    /// <param name="objName"></param>
    /// <param name="objValue"></param>
    public void WriteLine(string objName, object objValue)
    {
        if (objValue == null) objValue = "(null)";


        var append = string.Empty;
        if (!string.IsNullOrEmpty(objName)) append = objName + ": ";

        WriteLine(append + objValue);
    }

    /// <summary>
    /// Writes a list with optional numbering and a header
    /// </summary>
    /// <param name="what">Header text for the list</param>
    /// <param name="list">List of string items to write</param>
    /// <param name="isNumbered">Whether to prefix each item with its number</param>
    public void WriteNumberedList(string what, List<string> list, bool isNumbered)
    {
        WriteLineDelegate.Invoke(what + ":", []);
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
        list.ForEach(d => WriteLine(d));
    }
}