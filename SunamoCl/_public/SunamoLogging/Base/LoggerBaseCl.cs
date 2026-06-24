namespace SunamoCl._public.SunamoLogging.Base;

public abstract class LoggerBaseCl
{
    protected Action<string, string[]> WriteLineDelegate { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    protected LoggerBaseCl()
    {
    }

    public LoggerBaseCl(Action<string, string[]> writeLineDelegate)
    {
        WriteLineDelegate = writeLineDelegate;
    }

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
        WriteLineDelegate.Invoke(string.Join(separator, items), []);
    }

    public void WriteArgs(params string[] args)
    {
        WriteLineDelegate.Invoke(string.Join(";", args), []);
    }

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

    public void WriteLine(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(text, args);
    }

    public void WriteLineNull(string text, params string[] args)
    {
        if (IsActive) WriteLineDelegate.Invoke(SH.NullToStringOrDefault(text), args);
    }

    public void WriteLine(string text)
    {
        if (text != null) WriteLine(text, Array.Empty<string>());
    }

    public void WriteLine(string objectName, object objectValue)
    {
        if (objectValue == null) objectValue = "(null)";

        var append = string.Empty;
        if (!string.IsNullOrEmpty(objectName)) append = objectName + ": ";

        WriteLine(append + objectValue);
    }

    public void WriteNumberedList(string header, List<string> list, bool isNumbered)
    {
        WriteLineDelegate.Invoke(header + ":", []);
        for (var i = 0; i < list.Count; i++)
            if (isNumbered)
                WriteLine((i + 1).ToString(), list[i]);
            else
                WriteLine(list[i]);
    }

    public void WriteList(List<string> list)
    {
        list.ForEach(text => WriteLine(text));
    }
}
