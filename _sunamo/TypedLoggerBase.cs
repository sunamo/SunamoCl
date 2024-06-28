namespace SunamoCl;



/// <summary>
/// In difference with LoggerBase take type of message as enum
/// </summary>
internal abstract class TypedLoggerBase
{
    private static Type type = typeof(TypedLoggerBase);
    private Action<TypeOfMessage, string, string[]> _typedWriteLineDelegate;

    internal TypedLoggerBase(Action<TypeOfMessage, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    internal TypedLoggerBase()
    {

    }
#endif




    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="name"></param>
    /// <param name="v2"></param>
    internal void WriteLineFormat(string v1, params string[] name)
    {
        Ordinal(v1, name);
    }

    #region 
    internal void Success(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessage.Success, text, p);
    }

    internal void Error(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessage.Error, text, p);
    }
    internal void Warning(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessage.Warning, text, p);
    }

    internal void Appeal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessage.Appeal, text, p);
    }

    internal void Ordinal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessage.Ordinal, text, p);
    }

    internal void WriteLine(TypeOfMessage t, string m)
    {
        switch (t)
        {
            case TypeOfMessage.Error:
                Error(m);
                break;
            case TypeOfMessage.Warning:
                Warning(m);
                break;
            case TypeOfMessage.Information:
                Information(m);
                break;
            case TypeOfMessage.Ordinal:
                Ordinal(m);
                break;
            case TypeOfMessage.Appeal:
                Appeal(m);
                break;
            case TypeOfMessage.Success:
                Success(m);
                break;
            default:
                ThrowEx.NotImplementedCase(t);
                break;
        }
    }

    internal void Information(string text, params string[] p)
    {

        _typedWriteLineDelegate.Invoke(TypeOfMessage.Information, text, p);
    }
    #endregion
}
