namespace SunamoCl;


/// <summary>
/// Musí být v sunamo, jsou tu od něj odvozeny další třídy jako např. DebugLogger
/// </summary>
internal abstract class LoggerBase //: ILoggerBase
{
    // TODO: Make logger internal class as base and replace all occurences With Instance
    protected Action<string, string[]> _writeLineDelegate;
    internal bool IsActive = true;
    private static Type type = typeof(LoggerBase);
    private StringBuilder _sb = new StringBuilder();

    protected LoggerBase()
    {

    }

    //internal void DumpObject(string name, object o, DumpProvider d, params string[] onlyNames)
    //{
    //    var dump = RH.DumpAsString(new DumpAsStringArgs { name = name, o = o, d = d, onlyNames = onlyNames.ToList() });//  , o, d, onlyNames);
    //    WriteLine(dump);
    //    WriteLine(AllStrings.space);
    //}

    //internal void DumpObjects(string name, IList o, DumpProvider d, params string[] onlyNames)
    //{
    //    int i = 0;
    //    foreach (var item in o)
    //    {
    //        DumpObject(name + " #" + i, item, d, onlyNames);
    //        i++;
    //    }
    //}

    /// <summary>
    /// Only for debug purposes
    /// </summary>
    /// <param name = "v"></param>
    /// <param name = "args"></param>
    internal void ClipboardOrDebug(string v, params string[] args)
    {
#if DEBUG
        //DebugLogger.DebugWriteLine(TypeOfMessage.Appeal, v, args);
#else
//sb.AppendLine(TypeOfMessage.Appeal + ": " + string.Format(v, args));
//ClipboardHelper.SetText(sb.ToString());
#endif
    }

    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name = "v1"></param>
    /// <param name = "name"></param>
    /// <param name = "v2"></param>
    internal void WriteLineFormat(string v1, params string[] name)
    {
        WriteLine(v1, name);
    }

    internal LoggerBase(Action<string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

    internal void WriteCount(string collectionName, IList list)
    {
        WriteLine(collectionName + " count: " + list.Count);
    }

    internal void WriteList(string collectionName, List<string> list)
    {
        WriteLine(collectionName + " elements:");
        WriteList(list);
    }

    internal void WriteListOneRow(List<string> item, string swd)
    {
#if DEBUG
        _writeLineDelegate.Invoke(string.Join(swd, item), EmptyArrays.Strings);
#endif
    }

    internal void WriteArgs(params string[] args)
    {
        _writeLineDelegate.Invoke(/*SHJoinPairs.JoinPairs(args)*/ string.Join(";", args), EmptyArrays.Strings);
    }

    internal bool IsInRightFormat(string text, params string[] args)
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



    internal void WriteLine(string text, params string[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(text, args);
        }
    }

    internal void WriteLineNull(string text, params string[] args)
    {
        if (IsActive)
        {
            _writeLineDelegate.Invoke(SHSE.NullToStringOrDefault(text), args);
        }
    }

    /// <summary>
    /// for compatibility with CL.WriteLine
    /// </summary>
    /// <param name = "what"></param>
    internal void WriteLine(string what)
    {
        if (what != null)
        {
            WriteLine(what);
        }
    }

    /// <summary>
    /// Will auto append ": "
    /// </summary>
    /// <param name="what"></param>
    /// <param name="text"></param>
    internal void WriteLine(string what, object text)
    {
        if (text == null)
        {
            text = Consts.nulled;
        }



        string append = string.Empty;
        if (!string.IsNullOrEmpty(what))
        {
            append = what + ": ";
        }

        WriteLine(append + text.ToString());

    }

    internal void WriteNumberedList(string what, List<string> list, bool numbered)
    {
        _writeLineDelegate.Invoke(what + AllStrings.colon, EmptyArrays.Strings);
        for (int i = 0; i < list.Count; i++)
        {
            if (numbered)
            {
                WriteLine((i + 1).ToString(), list[i]);
            }
            else
            {
                WriteLine(list[i]);
            }
        }
    }

    internal void WriteList(List<string> list)
    {
        list.ForEach(d => WriteLine(d));
    }


}
