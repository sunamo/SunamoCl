namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
///     Musí být v sunamo, jsou tu od něj odvozeny další třídy jako např. DebugLogger
/// </summary>
public abstract class LoggerBaseCl //: ILoggerBase
{
    private static Type _type = typeof(LoggerBaseCl);

    private StringBuilder _sb = new();

    // TODO: Make logger public class as base and replace all occurences With Instance
    protected Action<string, string[]> _writeLineDelegate;
    public bool IsActive = true;

    protected LoggerBaseCl()
    {
    }

    public LoggerBaseCl(Action<string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

    /// <summary>
    ///     Only for debug purposes
    /// </summary>
    /// <param name="v"></param>
    /// <param name="args"></param>
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
    /// <param name="formatString"></param>
    /// <param name="args"></param>
    public void WriteLineFormat(string formatString, params string[] args)
    {
        WriteLine(formatString, args);
    }

    public void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    public void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    public void WriteListOneRow(List<string> items, string separator)
    {
        //#if DEBUG
        _writeLineDelegate.Invoke(string.Join(separator, items), []);
        //#endif
    }

    public void WriteArgs(params string[] args)
    {
        _writeLineDelegate.Invoke( /*SHJoinPairs.JoinPairs(args)*/ string.Join(";", args), []);
    }

    public bool IsInRightFormat(string text, params string[] args)
    {
        try
        {
            _writeLineDelegate.Invoke(text, args);
        }
        catch (Exception ex)
        {
            ThrowEx.CustomWithStackTrace(ex);
            return false;
        }

        return true;
    }


    public void WriteLine(string text, params string[] args)
    {
        if (IsActive) _writeLineDelegate.Invoke(text, args);
    }

    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive) _writeLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
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

    public void WriteNumberedList(string what, List<string> list, bool isNumbered)
    {
        _writeLineDelegate.Invoke(what + ":", []);
        for (var i = 0; i < list.Count; i++)
            if (isNumbered)
                WriteLine((i + 1).ToString(), list[i]);
            else
                WriteLine(list[i]);
    }

    public void WriteList(List<string> list)
    {
        list.ForEach(d => WriteLine(d));
    }
}